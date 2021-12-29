// <copyright file="CacheService.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>
using CSB.Core.Utilities.Redis.Services;
using System;

namespace CSB.Core.Utilities.Caching.Redis.Services
{
    /// <summary>
    /// Önbellekleme işlevlerini sağlayan servis.
    /// </summary>
    internal sealed class CacheService : ICacheService
    {
        private readonly IRedisService redisService;

        /// <summary>
        /// <see cref="IRedisService"/> arabiriminin tanımını uygun Redis servisini kullanarak önbellekleme işlevleri için sınıfın bir örneğini oluşturur.
        /// </summary>
        /// <param name="redisService">Redis işelvlerini sağlayan servis.</param>
        public CacheService(IRedisService redisService)
        {
            this.redisService = redisService;
        }

        /// <summary>
        /// Parametre olarak verilen veri anahtarının önbellekte bulunması durumunda ilgili anahtar ve değerini siler.
        /// </summary>
        /// <param name="key">Silinecek olan verinin anahtar değeri.</param>
        /// <returns>Veri anahtarının bulunması durumunda true aksi durumda false döndürür.</returns>
        public bool Delete(string key)
        {
            return this.redisService.Delete(key);
        }

        /// <summary>
        /// Redis önbellek veritabanında bulunan tüm verileri siler.
        /// </summary>
        public void Flush()
        {
            this.redisService.Flush();
        }

        /// <summary>
        /// Parametre olarak verilen anahtara uygun önbellek verisini getirir.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Anahtara uygun karakter dizesi.</returns>
        public string Get(string key)
        {
            return this.redisService.GetCache(key);
        }

        /// <summary>
        /// Parametre olarak verilen anahtara uygun önbellek verisini getirir.
        /// </summary>
        /// <typeparam name="T">Önbellekte bulunan verinin tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Anahtara uygun veri.</returns>
        public T Get<T>(string key)
        {
            return this.redisService.GetCache<T>(key);
        }

        /// <summary>
        /// Parametre olarak verilen veri anahtarına uygun veri bulunması durumunda ilgili veriyi, olmaması durumunda ilgili işlem sonucunu önbelleğe kaydederek ilgili değeri döndürür.
        /// </summary>
        /// <typeparam name="T">Verinin tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="action">Veriyi oluşturan işlem.</param>
        /// <param name="timeSpan">Muhafaza edilecek süre.</param>
        /// <returns>T tipinde veri.</returns>
        public T GetFromCacheOrRun<T>(string key, Func<T> action, TimeSpan timeSpan = default)
        {
            T result = this.redisService.GetCache<T>(key);
            if (result == null)
            {
                T invokeResult = action.Invoke();
                this.redisService.SetCache(key, invokeResult, timeSpan);
                return invokeResult;
            }

            return result;
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        public void Set(string key, string value)
        {
            this.redisService.SetCache<string>(key, value, default);
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi belirli bir süre için önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        /// <param name="timeSpan">Muhafaza edilecek süre.</param>
        public void Set(string key, string value, TimeSpan timeSpan)
        {
            this.redisService.SetCache<string>(key, value, timeSpan);
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili T tipinde veriyi önbelleğe kaydeder.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        public void Set<T>(string key, T value)
        {
            this.redisService.SetCache<T>(key, value, default);
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi belirli bir süre için önbelleğe kaydeder.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        /// <param name="timeSpan">Muhafaza edilecek süre.</param>
        public void Set<T>(string key, T value, TimeSpan timeSpan)
        {
            this.redisService.SetCache<T>(key, value, timeSpan);
        }
    }
}