// <copyright file="IGeospatialData.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

namespace CSB.Core.Utilities.Geospatial
{
    /// <summary>
    /// Coğrafi mekansal verilerinin tanımını içeren arabirim.
    /// </summary>
    public interface IGeospatialData
    {
        /// <summary>
        /// Coğrafi mekansal verinin boylam bilgisi.
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Coğrafi mekansal verinin enlem bilgisi.
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// Coğrafi mekansal verinin adı.
        /// </summary>
        string Name { get; }
    }
}