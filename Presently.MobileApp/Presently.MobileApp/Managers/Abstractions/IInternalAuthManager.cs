using System.Threading.Tasks;

namespace Presently.MobileApp.Managers.Abstractions
{
    public interface IInternalAuthManager
    {
        Task SaveSessionToken(string accessToken, long expiresAt);
        Task<bool> IsSessionValid();
        Task EnsureSessionIsValid();
        Task<string> GetAuthToken();
        void ClearAuthData();
    }
}