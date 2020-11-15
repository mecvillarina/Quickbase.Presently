namespace Presently.Common.Models
{
    public class AuthToken
    {
        public string AccessToken { get; set; }
        public long ExpireAt { get; set; }
    }
}