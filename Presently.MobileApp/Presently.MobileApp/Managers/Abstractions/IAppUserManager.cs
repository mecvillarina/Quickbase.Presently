using Presently.MobileApp.Managers.Entities;
using System.Threading.Tasks;

namespace Presently.MobileApp.Managers.Abstractions
{
    public interface IAppUserManager
    {
        Task GetProfile();
        AppUserEntity GetProfileLocally();
        void Clear();
    }
}