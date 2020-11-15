using Presently.MobileApp.Managers.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace Presently.MobileApp.Managers.Base
{
    public class AuthenticatedManagerBase : ManagerBase
    {
        protected readonly IInternalAuthManager AuthManager;
        public AuthenticatedManagerBase(IConnectivity connectivity, 
            IServiceMapper mapper, 
            IInternalAuthManager authManager) : base(connectivity, mapper)
        {
            AuthManager = authManager;
        }

        public Task EnsureSessionIsValid() => AuthManager.EnsureSessionIsValid();
        public Task<string> GetAccessToken() => AuthManager.GetAuthToken();
    }
}
