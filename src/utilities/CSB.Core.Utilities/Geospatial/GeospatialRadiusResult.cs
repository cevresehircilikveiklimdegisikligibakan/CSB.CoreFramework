// <copyright file="GeospatialRadiusResult.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

namespace CSB.Core.Utilities.Geospatial
{
    /// <summary>
    /// Coğrafi mekansal veriler arasında yapılan etki alan sonucunu temsil eden sınıf.
    /// </summary>
    public record GeospatialRadiusResult : IGeospatialRadiusResult
    {
        /// <summary>
        /// Coğrafi mekansal veri ile etki alan araması sonucu mesafe, karma değer ve ölçü birimi ile sınıfın bir örneğini oluşturur.
        /// </summary>
        /// <param name="geospatialData">Coğrafi mekansal veri.</param>
        /// <param name="distance">Mesafe.</param>
        /// <param name="hash">Karma değeri.</param>
        /// <param name="geospatialUnit">Ölçü birimi.</param>
        public GeospatialRadiusResult(IGeospatialData geospatialData, double? distance = null, long? hash = null, GeospatialUnit? geospatialUnit = null)
        {
            this.GeospatialData = geospatialData;
            this.Distance = distance;
            this.Hash = hash;
            this.GeospatialUnit = geospatialUnit;
        }

        /// <summary>
        /// Coğrafi mekansal veri.
        /// </summary>
        public IGeospatialData GeospatialData { get; }

        /// <summary>
        /// Mesafe.
        /// </summary>
        public double? Distance { get; }

        /// <summary>
        /// Karma değeri.
        /// </summary>
        public long? Hash { get; }

        /// <summary>
        /// Ölçü birimi.
        /// </summary>
        public GeospatialUnit? GeospatialUnit { get; }

        /// <summary>
        /// Mevcut olan ToString yönetminin varsayılan davranışı yerine coğrafi mekansal verinin adını döndürür.
        /// </summary>
        /// <returns>Coğrefi mekansal verinin adını döndürür.</returns>
        public override string ToString()
        {
            return this.GeospatialData?.Name;
        }
    }
}