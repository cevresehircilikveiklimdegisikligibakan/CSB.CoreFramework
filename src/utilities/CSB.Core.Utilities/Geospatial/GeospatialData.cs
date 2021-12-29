// <copyright file="GeospatialData.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

using System;

namespace CSB.Core.Utilities.Geospatial
{
    /// <summary>
    /// Coğrafi mekansal veriyi temsil eden sınıf.
    /// </summary>
    public struct GeospatialData : IGeospatialData, IEquatable<GeospatialData>
    {
        /// <summary>
        /// Coğrafi mekansal verinin adı, enlem ve boylam bilgisi ile yapının bir örneğini oluşturur.
        /// </summary>
        /// <param name="name">Verinin adı.</param>
        /// <param name="longitude">Boylam bilgisi.</param>
        /// <param name="latitude">Enlem bilgisi.</param>
        public GeospatialData(string name, double longitude, double latitude)
        {
            this.Name = name;
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        /// <summary>
        /// Boylam bilgisi.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Enlem bilgisi.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Verinin adı.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// İki coğrafi mekansal verinin aynı konumda olup olmadığını enlem ve boylam bakımından denetler.
        /// </summary>
        /// <param name="geospatialData">Coğrafi mekansal veri.</param>
        /// <param name="otherGeospatialData">Karşılaştırılan coğrafi mekansal veri.</param>
        /// <returns>Aynı olması durumunda true aksi durumda false döndürür.</returns>
        public static bool operator ==(GeospatialData geospatialData, GeospatialData otherGeospatialData)
        {
            return geospatialData.Longitude == otherGeospatialData.Longitude && geospatialData.Latitude == otherGeospatialData.Latitude && geospatialData.Name == otherGeospatialData.Name;
        }

        /// <summary>
        /// İki coğrafi mekansal verinin farklı konumda olup olmadığını enlem ve boylam bakımından denetler.
        /// </summary>
        /// <param name="geospatialData">Coğrafi mekansal veri.</param>
        /// <param name="otherGeospatialData">Karşılaştırılan coğrafi mekansal veri.</param>
        /// <returns>Farklı olması durumunda true aksi durumda false döndürür.</returns>
        public static bool operator !=(GeospatialData geospatialData, GeospatialData otherGeospatialData)
        {
            return geospatialData.Longitude != otherGeospatialData.Longitude || geospatialData.Latitude != otherGeospatialData.Latitude || geospatialData.Name != otherGeospatialData.Name;
        }

        /// <summary>
        /// Mevcut olan ToString yönetminin varsayılan davranışı yerine enlem ve boylam bilgisi döndürür.
        /// </summary>
        /// <returns>Verinin enlem ve boylam bilgisini döndürür.</returns>
        public override string ToString()
        {
            return $"{this.Longitude} {this.Latitude} - {this.Name}";
        }

        /// <summary>
        /// Mevcut olan ToString yönteminin varsayılan davranışı yerine enlem ve boylam bilgilerinin karma değerlerinin dışlayıcı veya (XOR) değerini döndürür.
        /// </summary>
        /// <returns>Enlem ve boylam bilgilerinin karma değerlerinin dışlayıcı veya (XOR) değerini döndürür.</returns>
        public override int GetHashCode()
        {
            return (this.Longitude.GetHashCode() ^ this.Latitude.GetHashCode()) ^ this.Name.GetHashCode(StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Mevcut olan Eqauls yönteminin varsayılan davranışı yerine enlem ve boylam bilgilerine istinaden parametre olarak verilen nesnenin aynı konum olup olmadığını döndürür.
        /// </summary>
        /// <param name="obj">Karşılaştırılacak olan nesne.</param>
        /// <returns>Karşılaştırılan nesne ile aynı konum olması durumunda true aksi durumda false döndürür.</returns>
        public override bool Equals(object obj)
        {
            return obj is IGeospatialData other && this.Equals(other);
        }

        /// <summary>
        /// Aynı yapı tipinde olan verinin aynı konumda olup olmadığı bilgisini döndürür.
        /// </summary>
        /// <param name="other">Karşılaştırılacak olan coğrafi mekansal veri.</param>
        /// <returns>Aynı konum olması durumunda true aksi durumda false döndürür.</returns>
        public bool Equals(IGeospatialData other)
        {
            return this == (GeospatialData)other;
        }

        /// <summary>
        /// Aynı yapı tipinde olan verinin aynı konumda olup olmadığı bilgisini döndürür.
        /// </summary>
        /// <param name="other">Karşılaştırılacak olan coğrafi mekansal veri.</param>
        /// <returns>Aynı konum olması durumunda true aksi durumda false döndürür.</returns>
        public bool Equals(GeospatialData other)
        {
            return this == other;
        }
    }
}