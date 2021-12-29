namespace CSB.Core.Utilities.Geospatial
{
    public interface IGeospatialSevice
    {
        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında adı bulunan 2 coğrafi mekan arası uzaklığı ilgili ölçü birimine göre döndürür.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="from">Başlangıç mekan.</param>
        /// <param name="to">Bitiş mekan.</param>
        /// <param name="geospatialUnit">Uzaklık için kullanılacak olan ölçü birimi.</param>
        /// <returns>2 coğrafi mekan arasında bulunan uzaklığı ilgili ölçü birimi cinsinden döndürür.</returns>
        double? GetGeoDistance(string key, string from, string to, GeospatialUnit geospatialUnit);

        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında bulunan ilgili coğrafi mekansal verilerin adları ile eşleşen verilere ait karma değerlerini döndürür.
        /// Verinin konum bilgileri ile ilgili veri kaybı yaşamamak adına hassasiyet adına konum bilgisi muhafaza edilir.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="names">Coğrafi mekansal verilerin adlarıı.</param>
        /// <returns>Coğrafi mekansal verilerin konum bilgilerinin karma değerleri.</returns>
        string[] GetGeoHash(string key, string[] names);

        /// <summary>
        /// Parametre olarak verilen anahtar kullanılarak coğrafi mekansal veriler arasında bulunan ilgili coğrafi mekansal verilerin adlarıı ile eşleşen verileri döndürür.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="names">Coğrafi mekansal verilerin adları.</param>
        /// <returns>Coğrefi mekansal verileri döndürür.</returns>
        IGeospatialData[] GetGeoLocation(string key, string[] names);

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
        IGeospatialRadiusResult[] GetGeoRadius(string key, string name, double radius, int count = -1, GeospatialUnit geospatialUnit = GeospatialUnit.Kilometer, GeospatialRadiusOption geospatialRadiusOption = GeospatialRadiusOption.Default, bool isAscending = true);

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
        IGeospatialRadiusResult[] GetGeoRadius(string key, double longitude, double latitude, int radius, int count, GeospatialUnit geospatialUnit = GeospatialUnit.Kilometer, GeospatialRadiusOption geospatialRadiusOption = GeospatialRadiusOption.Default, bool isAscending = true);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili coğrafi mekansal veriyi önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="geospatialData">Kaydedilecek coğrafi mekansal veri.</param>
        void SetGeo(string key, IGeospatialData geospatialData);

        /// <summary>
        /// Parametre olarak verilen anahtarı kullanarak ilgili coğrafi mekansal verileri önbelleğe kaydeder.
        /// </summary>
        /// <param name="key">Veri anahtarı.</param>
        /// <param name="geospatialData">Kaydedilecek coğrafi mekansal veriler.</param>
        void SetGeo(string key, IGeospatialData[] geospatialData);
    }
}