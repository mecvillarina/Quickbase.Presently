using System;
using System.Runtime.Serialization;

namespace Presently.MobileApp.Common.Exceptions
{
    [Serializable]
    public class InvalidAuthTokenException : Exception
    {
        public InvalidAuthTokenException()
        {
        }

        public InvalidAuthTokenException(string message) : base(message)
        {
        }

        public InvalidAuthTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAuthTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
