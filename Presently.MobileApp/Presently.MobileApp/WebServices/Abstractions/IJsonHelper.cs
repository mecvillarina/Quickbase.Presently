using System.IO;

namespace Presently.MobileApp.WebServices.Abstractions
{
    public interface IJsonHelper
    {
        T DeserializeObjectFromStream<T>(Stream stream);

        void SerializeObjectToStream<T>(T value, Stream stream);
    }
}
