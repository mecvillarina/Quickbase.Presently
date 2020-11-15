using Newtonsoft.Json;
using Presently.MobileApp.WebServices.Abstractions;
using System;
using System.IO;
using System.Text;

namespace Presently.MobileApp.WebServices.Utilities
{
    public class JsonHelper : IJsonHelper
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        public T DeserializeObjectFromStream<T>(Stream stream)
        {
            if (!stream.CanRead)
                throw new InvalidOperationException("Stream cannot be read.");

            using (var sr = new StreamReader(stream))
            using (var tr = new JsonTextReader(sr))
                return _serializer.Deserialize<T>(tr);
        }

        public void SerializeObjectToStream<T>(T value, Stream stream)
        {
            if (!stream.CanWrite)
                throw new InvalidOperationException("Stream cannot be written.");

            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.Indented })
            {
                _serializer.Serialize(jtw, value);
                jtw.Flush();
            }
        }
    }
}
