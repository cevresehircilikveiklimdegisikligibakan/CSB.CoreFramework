// <copyright file="RedisOptions.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

using System;

namespace CSB.Core.Utilities.Redis.Entities
{
    /// <summary>
    /// Redis önbellek işlemeleri için konfigürasyon bilgileri.
    /// </summary>
    internal record RedisOptions
    {
        /// <summary>
        /// Bağlantının yapılacağı veritabanı.
        /// </summary>
        public int DatabaseId { get; set; }

        /// <summary>
        /// Veri bağlantı cümlesi.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Varsayılan zaman aşımı süresi.
        /// </summary>
        public TimeSpan Timeout { get; set; }
    }
}