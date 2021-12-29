// <copyright file="IRedisService.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

using System;

namespace CSB.Core.Utilities.Redis.Services
{
    /// <summary>
    /// Redis servisinin tanımını içeren arabirim.
    /// </summary>
    public interface IRedisService
    {
        /// <summary>
        /// Zaman aşımı süresi.
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Redis önbellek veritabanında bulunan tüm verileri siler.
        /// </summary>
        void Flush();

        /// <summary>
        /// Parametre olarak verilen veri anahtarının önbellekte bulunması durumunda ilgili anahtar ve değerini siler.
        /// </summary>
        /// <param name="key">Silinecek olan verinin anahtar değeri.</param>
        /// <returns>Veri anahtarının bulunması durumunda true aksi durumda false döndürür.</returns>
        bool Delete(string key);

        /// <summary>
        /// Parametre olarak verilen anahtara uygun karakter dizesini önbellekten getirir.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Veri anahtarına uygun karakter dizesi.</returns>
        string GetCache(string key);

        /// <summary>
        /// Parametre olarak verilen anahtara uygun karakter veriyi önbellekten getirir.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Veri anahtarına uygun veri.</returns>
        T GetCache<T>(string key);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi belirli bir süre için önbelleğe kaydeder.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        /// <param name="timeout">Muhafaza edilecek süre.</param>
        void SetCache<T>(string key, T value, TimeSpan timeout);

    }
}