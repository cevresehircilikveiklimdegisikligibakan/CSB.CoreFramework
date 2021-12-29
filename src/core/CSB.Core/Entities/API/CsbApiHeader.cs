using System;
using System.Runtime.Serialization;

namespace CSB.Core.Entities.API
{
    /// <summary>
    /// Çevre Şehircilik ve İklim Değişikliği Bakanlığı SMS servisi için gerekli header bilgilerini temsil eden sınıf.
    /// </summary>
    [Serializable]
    public class CsbApiHeader
    {
        /// <summary>
        /// Kullanıcı adı.
        /// </summary>
        [DataMember]
        public string KullaniciAdi { get; set; }

        /// <summary>
        /// Kullanıcının IP adresi.
        /// </summary>
        [DataMember]
        public string KullaniciIp { get; set; }

        /// <summary>
        /// İşlem kodu.
        /// </summary>
        [DataMember]
        public string IslemKodu { get; set; }

        /// <summary>
        /// İşlem zamanı.
        /// </summary>
        [DataMember]
        public string IslemZamani { get; set; }
    }
}