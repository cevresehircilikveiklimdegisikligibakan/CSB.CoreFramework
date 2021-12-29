// <copyright file="ICacheService.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

using System;

namespace CSB.Core.Utilities.Caching
{
    /// <summary>
    /// Önbellek işlemlerini gerçekleştiren servis tanımını içeren arabirim.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Parametre olarak verilen anahtara uygun önbellek verisini getirir.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Anahtara uygun karakter dizesi.</returns>
        string Get(string key);

        /// <summary>
        /// Parametre olarak verilen anahtara uygun önbellek verisini getirir.
        /// </summary>
        /// <typeparam name="T">Önbellekte bulunan verinin tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <returns>Anahtara uygun veri.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        void Set(string key, string value);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi belirli bir süre için önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        /// <param name="timeSpan">Muhafaza edilecek süre.</param>
        void Set(string key, string value, TimeSpan timeSpan);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili T tipinde veriyi önbelleğe kaydeder.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili veriyi belirli bir süre için önbelleğe kaydeder.
        /// </summary>
        /// <typeparam name="T">Veri tipi.</typeparam>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="value">Kaydedilecek veri.</param>
        /// <param name="timeSpan">Muhafaza edilecek süre.</param>
        void Set<T>(string key, T value, TimeSpan timeSpan);

        /// <summary>
        /// Uygulama için ayrılmış olan önbelleği temizler. Bütün anahtar ve değerler silinir.
        /// </summary>
        void Flush();

        /// <summary>
        /// Parametre olarak verilen veri anahtarının önbellekte bulunması durumunda ilgili anahtar ve değerini siler.
        /// </summary>
        /// <param name="key">Silinecek olan verinin anahtar değeri.</param>
        /// <returns>Veri anahtarının bulunması durumunda true aksi durumda false döndürür.</returns>
        bool Delete(string key);
    }
}