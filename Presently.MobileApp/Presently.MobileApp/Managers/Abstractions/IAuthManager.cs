using Presently.MobileApp.Managers.Entities;
using System.Threading.Tasks;

namespace Presently.MobileApp.Managers.Abstractions
{
    public interface IAuthManager
    {
        void ClearAuthData();
        Task<bool> IsSessionValid();
        Task Login(AuthLoginRequestEntity reqEntity);
        Task GetProfile();
        AppUserEntity GetProfileLocally();
    }
}
