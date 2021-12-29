using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CSB.Core.Services
{
    internal class XMLSerializer : IXMLSerializer
    {
        public string Serialize<T>(T value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            try
            {
                using (StringWriter stringWriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stringWriter, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public T Deserialize<T>(string value, Encoding encoding = null)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return default;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlReaderSettings settings = new XmlReaderSettings();

                if (encoding != null)
                {
                    byte[] xmlBytes = encoding.GetBytes(value);
                    using MemoryStream memoryStream = new MemoryStream(xmlBytes);
                    using XmlReader xmlReader = XmlReader.Create(memoryStream, settings);
                    return (T)serializer.Deserialize(xmlReader);
                }
                else
                {
                    using StringReader textReader = new StringReader(value);
                    using XmlReader xmlReader = XmlReader.Create(textReader, settings);
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}