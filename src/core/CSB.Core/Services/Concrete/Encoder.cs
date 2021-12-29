using System.Text;

namespace CSB.Core.Services
{
    internal class Encoder : IEncoder
    {
        private readonly ISerializer _serializer;
        public Encoder(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public byte[] GetBytes<T>(T data)
        {
            var bytes = Encoding.UTF8.GetBytes(_serializer.Serialize(data));
            return bytes;
        }

        public T GetData<T>(byte[] bytes)
        {
            var serializedData = Encoding.UTF8.GetString(bytes);
            var data = _serializer.Deserialize<T>(serializedData);
            return data;
        }

    }
}