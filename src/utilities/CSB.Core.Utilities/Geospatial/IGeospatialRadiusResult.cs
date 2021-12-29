// <copyright file="IGeospatialRadiusResult.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

namespace CSB.Core.Utilities.Geospatial
{
    /// <summary>
    /// Coğrafi mekansal veriler arasında yapılan etki alanı hesaplama sonucunun tanımını belriten arabirim.
    /// </summary>
    public interface IGeospatialRadiusResult
    {
        /// <summary>
        /// Coğrafi mekansal veri.
        /// </summary>
        IGeospatialData GeospatialData { get; }

        /// <summary>
        /// Mesafe.
        /// </summary>
        double? Distance { get; }

        /// <summary>
        /// Karma değeri.
        /// </summary>
        long? Hash { get; }

        /// <summary>
        /// Mesafe için ullanılan ölçü birimi.
        /// </summary>
        GeospatialUnit? GeospatialUnit { get; }
    }
}