using CSB.Core.Utilities.Geospatial;
using StackExchange.Redis;
using System.Linq;

namespace CSB.Core.Utilities.Redis.Services
{
    internal sealed partial class RedisService : IRedisGeospatialService
    {
        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında adı bulunan 2 coğrafi mekan arası uzaklığı ilgili ölçü birimine göre döndürür.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="from">Başlangıç mekan.</param>
        /// <param name="to">Bitiş mekan.</param>
        /// <param name="geospatialUnit">Uzaklık için kullanılacak olan ölçü birimi.</param>
        /// <returns>2 coğrafi mekan arasında bulunan uzaklığı ilgili ölçü birimi cinsinden döndürür.</returns>
        public double? GetGeoDistance(string key, string from, string to, GeospatialUnit geospatialUnit)
        {
            GeoUnit geoUnit = this.ConvertUnit(geospatialUnit);

            return this.GetDatabase().GeoDistance(key, from, to, geoUnit);
        }

        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında bulunan ilgili coğrafi mekansal verilerin adları ile eşleşen verilere ait karma değerlerini döndürür.
        /// Verinin konum bilgileri ile ilgili veri kaybı yaşamamak adına hassasiyet adına konum bilgisi muhafaza edilir.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="names">Coğrafi mekansal verilerin adlarıı.</param>
        /// <returns>Coğrafi mekansal verilerin konum bilgilerinin karma değerleri.</returns>
        public string[] GetGeoHash(string key, string[] names)
        {
            RedisValue[] members = names.Select(name => new RedisValue(name)).ToArray();
            return this.GetDatabase().GeoHash(key, members);
        }

        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında bulunan ilgili coğrafi mekansal verilerin adlarıı ile eşleşen verileri döndürür.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="names">Coğrafi mekansal verilerin adları.</param>
        /// <returns>Coğrefi mekansal verileri döndürür.</returns>
        public IGeospatialData[] GetGeoLocation(string key, string[] names)
        {
            RedisValue[] members = names.Select(name => new RedisValue(name)).ToArray();
            GeoPosition?[] geoPositions = this.GetDatabase().GeoPosition(key, members);

            return this.GetGeospatialData(geoPositions, names);
        }

        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında etki alanı hesaplaması yaparak uygun koşullara sahip verileri döndürür.
        /// Veri seti içerisinde bulunan bir konumun verilen parametrelere uygun etki alanı içerisinde kalan konumları döndürür.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="name">Merkez olarak kullanılacak olan verinin adı.</param>
        /// <param name="radius">Çevre uzunluğu.</param>
        /// <param name="count">Döndürülecek olan azami kayıt sayısı.</param>
        /// <param name="geospatialUnit">Çevre uzunluğunun ölçü birimi. Varsayılan kilometre olarak kullanılmaktadır.</param>
        /// <param name="geospatialRadiusOption">Döndürülecek olan bilgiler. Varsayılan olarak koordinat ve uzaklık bilgileri kullanılmaktadır.</param>
        /// <param name="isAscending">Döndürülen verilerin alfabetik olarak döndürülüp döndürülmeyeceği.</param>
        /// <returns>Etki alanı içerisinde kalan coğrafi mekansal verileri döndürür.</returns>
        public IGeospatialRadiusResult[] GetGeoRadius(string key, string name, double radius, int count = -1, GeospatialUnit geospatialUnit = GeospatialUnit.Kilometer, GeospatialRadiusOption geospatialRadiusOption = GeospatialRadiusOption.Default, bool isAscending = true)
        {
            GeoUnit geoUnit = this.ConvertUnit(geospatialUnit);
            GeoRadiusOptions geoRadiusOptions = this.ConvertRadiusOptions(geospatialRadiusOption);

            GeoRadiusResult[] results = this.GetDatabase().GeoRadius(key, name, radius, geoUnit, count, isAscending ? Order.Ascending : Order.Descending, geoRadiusOptions);

            return this.GetGeospatialRadiusResult(results, geospatialUnit);
        }

        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında etki alanı hesaplaması yaparak uygun koşullara sahip verileri döndürür.
        /// Veri seti içerisinde bulunan bir konumun verilen parametrelere uygun etki alanı içerisinde kalan konumları döndürür.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="longitude">Merkez olarak kullanılacak olan konumun boylam bilgisi.</param>
        /// <param name="latitude">Merkez olarak kullanılacak olan konumun enlem bilgisi.</param>
        /// <param name="radius">Çevre uzunluğu.</param>
        /// <param name="count">Döndürülecek olan azami kayıt sayısı.</param>
        /// <param name="geospatialUnit">Çevre uzunluğunun ölçü birimi. Varsayılan kilometre olarak kullanılmaktadır.</param>
        /// <param name="geospatialRadiusOption">Döndürülecek olan bilgiler. Varsayılan olarak koordinat ve uzaklık bilgileri kullanılmaktadır.</param>
        /// <param name="isAscending">Döndürülen verilerin alfabetik olarak döndürülüp döndürülmeyeceği.</param>
        /// <returns>Etki alanı içerisinde kalan coğrafi mekansal verileri döndürür.</returns>
        public IGeospatialRadiusResult[] GetGeoRadius(string key, double longitude, double latitude, int radius, int count, GeospatialUnit geospatialUnit = GeospatialUnit.Kilometer, GeospatialRadiusOption geospatialRadiusOption = GeospatialRadiusOption.Default, bool isAscending = true)
        {
            GeoUnit geoUnit = this.ConvertUnit(geospatialUnit);
            GeoRadiusOptions geoRadiusOptions = this.ConvertRadiusOptions(geospatialRadiusOption);

            GeoRadiusResult[] results = this.GetDatabase().GeoRadius(key, longitude, latitude, radius, geoUnit, count, isAscending ? Order.Ascending : Order.Descending, geoRadiusOptions);

            return this.GetGeospatialRadiusResult(results, geospatialUnit);
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili coğrafi mekansal veriyi önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="geospatialData">Kaydedilecek coğrafi mekansal veri.</param>
        public void SetGeo(string key, IGeospatialData geospatialData)
        {
            this.GetDatabase().GeoAdd(key, new GeoEntry(geospatialData.Longitude, geospatialData.Latitude, geospatialData.Name));
        }

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili coğrafi mekansal verileri önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="geospatialData">Kaydedilecek coğrafi mekansal veriler.</param>
        public void SetGeo(string key, IGeospatialData[] geospatialData)
        {
            GeoEntry[] geoEntries = geospatialData.Select(data => new GeoEntry(data.Longitude, data.Latitude, data.Name)).ToArray();
            this.GetDatabase().GeoAdd(key, geoEntries);
        }

        /// <summary>
        /// Parametre olarak verilen Redis özelinde olan etki alan sonuçlarını <see cref="GeospatialRadiusResult"/> türüne dönüştürür.
        /// </summary>
        /// <param name="results">Redis özelinde olan etki alan sonuç dizesi.</param>
        /// <param name="geospatialUnit">Hesaplamada kullanılan ölçü birimi.</param>
        /// <returns>Etki alan sonuçlarını döndürür.</returns>
        private IGeospatialRadiusResult[] GetGeospatialRadiusResult(GeoRadiusResult[] results, GeospatialUnit geospatialUnit)
        {
            IGeospatialRadiusResult[] geospatialRadiusResults = null;
            if (results != null && results.Any())
            {
                geospatialRadiusResults = new IGeospatialRadiusResult[results.Length];
                for (int resultIndex = 0; resultIndex < results.Length; resultIndex++)
                {
                    GeoRadiusResult geoRadiusResult = results[resultIndex];
                    GeoPosition? geoPosition = geoRadiusResult.Position;
                    if (geoPosition.HasValue)
                    {
                        GeospatialData geospatialData = new GeospatialData(geoRadiusResult.Member.ToString(), geoRadiusResult.Position.Value.Longitude, geoRadiusResult.Position.Value.Latitude);
                        geospatialRadiusResults[resultIndex] = new GeospatialRadiusResult(geospatialData, geoRadiusResult.Distance, geoRadiusResult.Hash, geospatialUnit);
                    }
                }
            }

            return geospatialRadiusResults;
        }

        /// <summary>
        /// Redis özelinde olan konum verilerini <see cref="GeospatialData"/> tipinde coğrafi mekansal verilere dönüştürür.
        /// </summary>
        /// <param name="geoPositions">Redis özelinde konumlar.</param>
        /// <param name="names">Konum verilerini temsil eden adlar.</param>
        /// <returns>Coğrafi mekansal verileri döndürür.</returns>
        private IGeospatialData[] GetGeospatialData(GeoPosition?[] geoPositions, string[] names)
        {
            IGeospatialData[] geospatialDatas = null;
            if (geoPositions != null && geoPositions.Any())
            {
                geospatialDatas = new IGeospatialData[geoPositions.Length];
                for (int geoPositionIndex = 0; geoPositionIndex < geoPositions.Length; geoPositionIndex++)
                {
                    GeoPosition? geoPosition = geoPositions[geoPositionIndex];
                    if (geoPosition.HasValue)
                    {
                        geospatialDatas[geoPositionIndex] = new GeospatialData(names[geoPositionIndex], geoPosition.Value.Longitude, geoPosition.Value.Latitude);
                    }
                }
            }

            return geospatialDatas;
        }

        /// <summary>
        /// Coğrafi mekansal veri ölçü birimini Redis özelinde ölçü birimine dönüştürür.
        /// </summary>
        /// <param name="geospatialUnit">Coğrafi mekansal veri ölçü birimi.</param>
        /// <returns>Redis ölçü birimi.</returns>
        private GeoUnit ConvertUnit(GeospatialUnit geospatialUnit)
        {
            GeoUnit geoUnit;
            switch (geospatialUnit)
            {
                default:
                case GeospatialUnit.Meter:
                    geoUnit = GeoUnit.Meters;
                    break;
                case GeospatialUnit.Kilometer:
                    geoUnit = GeoUnit.Kilometers;
                    break;
            }

            return geoUnit;
        }

        /// <summary>
        /// Coğrafi mekansal veriler için etki alanı hesaplamaları için kullanılacak seçenekleri Redis özelinde etki alan seçeneklerine dönüştürür.
        /// </summary>
        /// <param name="geospatialRadiusOption">Coğrafi mekansal veriler için etki alanı hesaplamaları için kullanılacak seçenekler.</param>
        /// <returns>Redis özelinde etki alan seçenekleri.</returns>
        private GeoRadiusOptions ConvertRadiusOptions(GeospatialRadiusOption geospatialRadiusOption)
        {
            GeoRadiusOptions geoRadiusOptions;
            int radiusOptions = (int)geospatialRadiusOption;
            switch (radiusOptions)
            {
                case 0:
                    geoRadiusOptions = GeoRadiusOptions.None;
                    break;
                case 1:
                    geoRadiusOptions = GeoRadiusOptions.WithCoordinates;
                    break;
                case 2:
                    geoRadiusOptions = GeoRadiusOptions.WithDistance;
                    break;
                case 3:
                default:
                    geoRadiusOptions = GeoRadiusOptions.Default;
                    break;
                case 4:
                    geoRadiusOptions = GeoRadiusOptions.WithGeoHash;
                    break;
                case 5:
                    geoRadiusOptions = GeoRadiusOptions.WithCoordinates | GeoRadiusOptions.WithGeoHash;
                    break;
                case 6:
                    geoRadiusOptions = GeoRadiusOptions.WithDistance | GeoRadiusOptions.WithGeoHash;
                    break;
                case 7:
                    geoRadiusOptions = GeoRadiusOptions.WithCoordinates | GeoRadiusOptions.WithGeoHash | GeoRadiusOptions.WithDistance;
                    break;
            }

            return geoRadiusOptions;
        }

    }
}