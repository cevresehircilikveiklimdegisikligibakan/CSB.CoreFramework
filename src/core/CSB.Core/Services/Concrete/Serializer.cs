using Newtonsoft.Json;
using System;

namespace CSB.Core.Services
{
    internal class Serializer : ISerializer
    {
        /// <summary>
        /// Serileştirme / Ters serileştirme işlem konfigürasyonu.
        /// </summary>
        private JsonSerializerSettings settings;

        /// <summary>
        /// Parametre olarak verilen konfigürasyona uygun, sınıfın bir örneğini oluşturur.
        /// </summary>
        /// <param name="settings">Serileştirme / Ters serileştirme işlem konfigürasyonu.</param>
        public Serializer(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Ters serileştirme işlemini gerçekleştirir.
        /// </summary>
        /// <typeparam name="T">Verinin tipi.</typeparam>
        /// <param name="value">Ter serileştirilecek veri.</param>
        /// <returns>Veri.</returns>
        public T Deserialize<T>(string value)
        {
            if (this.settings == null)
            {
                this.settings = new JsonSerializerSettings();
                this.settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            }
            return JsonConvert.DeserializeObject<T>(value, this.settings);
        }


        /// <summary>
        /// Ters serileştirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="value">Ters serileştirilecek veri.</param>
        /// <param name="returnType">Verinin tipi.</param>
        /// <returns>Veri.</returns>
        public object Deserialize(string value, Type returnType)
        {
            return JsonConvert.DeserializeObject(value, returnType, this.settings);
        }

        /// <summary>
        /// Serileştirme işlemini gerçekleştirir.
        /// </summary>
        /// <typeparam name="T">Verinin tipi.</typeparam>
        /// <param name="value">Serileştirilecek veri.</param>
        /// <returns>Serileştirilmiş veri.</returns>
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, this.settings);
        }
    }
}