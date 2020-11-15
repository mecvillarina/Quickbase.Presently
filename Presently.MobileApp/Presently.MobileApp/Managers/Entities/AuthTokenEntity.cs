namespace Presently.MobileApp.Managers.Entities
{
    public class AuthTokenEntity
    {
        public string AccessToken { get; set; }
        public long ExpireAt { get; set; }
    }
}
