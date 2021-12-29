// <copyright file="RedisService.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

using CSB.Core.Services;
using CSB.Core.Utilities.Redis.Entities;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Net;

namespace CSB.Core.Utilities.Redis.Services
{
    /// <summary>
    /// Redis işlevlerini sağlayan servis.
    /// </summary>
    internal sealed partial class RedisService : IRedisService
    {
        private readonly RedisOptions options;
        private readonly ISerializer serializer;
        private readonly Lazy<ConnectionMultiplexer> connectionMultiplexer;

        /// <summary>
        /// <see cref="RedisOptions"/> sınıfının özelliklerini kullanarak ilgili sınıfın bir örneğini oluşturur.
        /// </summary>
        /// <param name="options">Konfigürasyon nesnesi.</param>
        /// <param name="serializer">Serileştirme / Ters Serileştirme işlemlerini gerçekleştiren nesne.</param>
        public RedisService(IOptions<RedisOptions> options, ISerializer serializer)
        {
            this.options = options.Value;
            this.serializer = serializer;
            this.Timeout = options.Value.Timeout;
            this.connectionMultiplexer = new Lazy<ConnectionMultiplexer>(this.CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Önebllek işlemleri için zaman aşımı süresi.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Redis önbellek veritabanında bulunan tüm verileri siler.
        /// </summary>
        public void Flush()
        {
            EndPoint[] endPoints = this.connectionMultiplexer.Value.GetEndPoints();
            foreach (EndPoint endPoint in endPoints)
            {
                IServer server = this.connectionMultiplexer.Value.GetServer(endPoint);
                server.FlushDatabase(this.options.DatabaseId);
            }
        }

        /// <summary>
        /// Parametre olarak verilen veri anahtarının önbellekte bulunması durumunda ilgili anahtar ve değerini siler.
        /// </summary>
        /// <param name="key">Silinecek olan verinin anahtar değeri.</param>
        /// <returns>Veri anahtarının bulunması durumunda true aksi durumda false döndürür.</returns>
        public bool Delete(string key)
        {
            return this.GetDatabase().KeyDelete(key);
        }

        /// <summary>
        /// Parametre olarak verilen anahtara uygun karakter dizesini önbellekten getirir.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Veri anahtarına uygun karakter dizesi.</returns>
        public string GetCache(string key)
        {
            RedisValue response = this.GetDatabase().StringGet(key);
            return response;
        }

        /// <summary>
        /// Parametre olarak verilen anahtara uygun karakter veriyi önbellekten getirir.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Veri anahtarına uygun veri.</returns>
        public T GetCache<T>(string key)
        {
            RedisValue response = this.GetDatabase().StringGet(key);
            if (!response.HasValue)
            {
                return default;
            }

            return this.serializer.Deserialize<T>(response);
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi belirli bir süre için önbelleğe kaydeder.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        /// <param name="timeout">Muhafaza edilecek süre.</param>
        public void SetCache<T>(string key, T value, TimeSpan timeout = default)
        {
            string redisValue;
            if (value is string)
            {
                redisValue = value.ToString();
            }
            else
            {
                redisValue = this.serializer.Serialize(value);
            }

            this.GetDatabase().StringSet(key, redisValue, timeout);
        }


        /// <summary>
        /// Redis önbellek veritabanı.
        /// </summary>
        /// <returns>Önbellek veritabanı.</returns>
        private IDatabase GetDatabase()
        {
            return this.connectionMultiplexer.Value.GetDatabase(this.options.DatabaseId);
        }

        /// <summary>
        /// Redis önbellek veri bağlantısını gerçekleştirir.
        /// </summary>
        /// <returns>Çoklu veri bağlantısı nesnesi.</returns>
        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(this.options.ConnectionString);
        }
    }
}