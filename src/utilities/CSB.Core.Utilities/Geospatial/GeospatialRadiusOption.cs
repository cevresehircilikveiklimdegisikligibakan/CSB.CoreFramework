// <copyright file="GeospatialRadiusOption.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

namespace CSB.Core.Utilities.Geospatial
{
    /// <summary>
    /// Coğrafi mekansal veriler için etki alanı hesaplamaları için kullanılacak seçenekleri belirten sayılandırma sabiti.
    /// </summary>
    public enum GeospatialRadiusOption
    {
        /// <summary>
        /// Ek bilgi içermeyecek.
        /// </summary>
        None = 0,

        /// <summary>
        /// Koordinat bilgisi içerecek.
        /// </summary>
        Coordinates = 1,

        /// <summary>
        /// Uzaklık bilgisi içerecek.
        /// </summary>
        Distance = 2,

        /// <summary>
        /// Karma değer bilgisi içerecek.
        /// </summary>
        Hash = 4,

        /// <summary>
        /// Varsayılan olarak koordinat ve uzaklık bilgilerini içerecek.
        /// </summary>
        Default = Coordinates | Distance,
    }
}