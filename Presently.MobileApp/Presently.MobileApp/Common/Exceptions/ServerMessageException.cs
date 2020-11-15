using System;
using System.Runtime.Serialization;

namespace Presently.MobileApp.Common.Exceptions
{
    [Serializable]
    public class ServerMessageException : Exception
    {
        public ServerMessageException()
        {
        }

        public ServerMessageException(string message) : base(message)
        {
        }

        public ServerMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServerMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
