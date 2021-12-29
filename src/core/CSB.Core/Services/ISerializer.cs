using System;

namespace CSB.Core.Services
{
    public interface ISerializer
    {
        /// <summary>
        /// Serileştirme işlemini gerçekleştirir.
        /// </summary>
        /// <typeparam name="T">Verinin tipi.</typeparam>
        /// <param name="value">Serileştirilecek veri.</param>
        /// <returns>Serileştirilmiş veri.</returns>
        string Serialize<T>(T value);

        /// <summary>
        /// Ters serileştirme işlemini gerçekleştirir.
        /// </summary>
        /// <typeparam name="T">Verinin tipi.</typeparam>
        /// <param name="value">Ters serileştirilecek veri.</param>
        /// <returns>Veri.</returns>
        T Deserialize<T>(string value);

        /// <summary>
        /// Ters serileştirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="value">Ters serileştirilecek veri.</param>
        /// <param name="returnType">Verinin tipi.</param>
        /// <returns>Veri.</returns>
        object Deserialize(string value, Type returnType);
    }
}