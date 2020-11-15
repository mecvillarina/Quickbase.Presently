using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Base;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.WebServices.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace Presently.MobileApp.Managers
{
    public class AuthManager : AuthenticatedManagerBase, IAuthManager
    {
        private readonly IInternalAuthManager _internalAuthManager;
        private readonly IAuthWebService _authWebService;

        public AuthManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager internalAuthManager,
            IAuthWebService authWebService) : base(connectivity, mapper, internalAuthManager)
        {
            _internalAuthManager = internalAuthManager;
            _authWebService = authWebService;
        }

        public void ClearAuthData() => _internalAuthManager.ClearAuthData();
        public Task<bool> IsSessionValid() => _internalAuthManager.IsSessionValid();

        public async Task Login(AuthLoginRequestEntity reqEntity)
        {
            EnsureInternetAvailable();

            try
            {
                var reqContract = Mapper.Map<AuthLoginRequestContract>(reqEntity);
                var authToken = await _authWebService.Login(reqContract);
                await _internalAuthManager.SaveSessionToken(authToken.AccessToken, authToken.ExpireAt);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

      
    }
}
