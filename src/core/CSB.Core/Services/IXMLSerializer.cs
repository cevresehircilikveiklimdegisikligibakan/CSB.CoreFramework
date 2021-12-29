using System.Text;

namespace CSB.Core.Services
{
    public interface IXMLSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string value, Encoding encoding = null);
    }
}