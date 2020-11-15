using Presently.Common.Abstractions;

namespace Presently.MobileApp.WebServices.DataContracts
{
    public class AuthTokenDataContract : IJsonDataContract
    {
        public string AccessToken { get; set; }
        public long ExpireAt { get; set; }
    }
}
