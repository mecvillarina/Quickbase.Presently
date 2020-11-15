using System;
using System.Runtime.Serialization;

namespace Presently.MobileApp.Common.Exceptions
{
    [Serializable]
    public class AuthInvalidCredentialException : Exception
    {
        public AuthInvalidCredentialException()
        {
        }

        public AuthInvalidCredentialException(string message) : base(message)
        {
        }

        public AuthInvalidCredentialException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthInvalidCredentialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
