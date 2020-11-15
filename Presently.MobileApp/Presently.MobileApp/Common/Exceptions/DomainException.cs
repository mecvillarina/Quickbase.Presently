using System;
using System.Runtime.Serialization;

namespace Presently.MobileApp.Common.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string message, string messageTitle) : base(message)
        {
            MessageTitle = messageTitle;
        }

        public DomainException(string message, string messageTitle, Exception innerException) : base(message, innerException)
        {
            MessageTitle = messageTitle;
        }

        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string MessageTitle { get; }
    }
}