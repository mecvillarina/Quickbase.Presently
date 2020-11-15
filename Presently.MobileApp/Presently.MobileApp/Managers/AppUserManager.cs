using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Base;
using Presently.MobileApp.Managers.Entities;
using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.DataObjects;
using Presently.MobileApp.WebServices.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace Presently.MobileApp.Managers
{
    public class AppUserManager : AuthenticatedManagerBase, IAppUserManager
    {
        private readonly IAuthWebService _authWebService;
        private readonly IAppUserRepository _appUserRepository;

        public AppUserManager(IConnectivity connectivity,
            IServiceMapper mapper, 
            IInternalAuthManager authManager,
            IAuthWebService authWebService,
            IAppUserRepository appUserRepository) : base(connectivity, mapper, authManager)
        {
            _authWebService = authWebService;
            _appUserRepository = appUserRepository;
        }

        public async Task GetProfile()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _authWebService.GetProfile(accessToken);
                var dataObject = Mapper.Map<AppUserDataObject>(contract);
                _appUserRepository.Clear();
                _appUserRepository.Add(dataObject);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public AppUserEntity GetProfileLocally()
        {
            var dataObject = _appUserRepository.FirstOrDefault(x => true);
            return Mapper.Map<AppUserEntity>(dataObject);
        }
    }
}
