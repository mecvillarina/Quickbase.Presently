using System;
using System.Runtime.Serialization;

namespace Presently.MobileApp.Common.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(
            string message,
            string method,
            int statusCode,
            string reasonPhrase) : this(message)
        {
            Method = method;
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public ApiException(
            string message,
            string method,
            int statusCode,
            string reasonPhrase,
            ApiErrorData errorData) : this(message, method, statusCode, reasonPhrase)
        {
            ErrorData = errorData;
        }

        protected ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApiErrorData ErrorData { get; }

        public string Method { get; }

        public string ReasonPhrase { get; }

        public int StatusCode { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ErrorData), ErrorData);
            info.AddValue(nameof(Method), Method);
            info.AddValue(nameof(ReasonPhrase), ReasonPhrase);
            info.AddValue(nameof(StatusCode), StatusCode);

            base.GetObjectData(info, context);
        }

        public override string ToString() => $"{Method} request failed ({StatusCode} {ReasonPhrase})";
    }
}