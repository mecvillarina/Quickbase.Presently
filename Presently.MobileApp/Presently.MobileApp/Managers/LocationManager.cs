using Presently.MobileApp.Common.Exceptions;
using Presently.MobileApp.Managers.Abstractions;
using Presently.MobileApp.Managers.Base;
using Presently.MobileApp.WebServices.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace Presently.MobileApp.Managers
{
    public class LocationManager : AuthenticatedManagerBase, ILocationManager
    {
        private readonly ILocationWebService _locationWebService;

        public LocationManager(IConnectivity connectivity, 
            IServiceMapper mapper, 
            IInternalAuthManager authManager,
            ILocationWebService locationWebService) : base(connectivity, mapper, authManager)
        {
            _locationWebService = locationWebService;
        }

        public async Task<string> AcquireAddress(double latitude, double longitude)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _locationWebService.AcquireAddress(latitude, longitude, accessToken);
                return contract.FormattedTextAddress;
            }
            catch (ApiException ex)
            {
                throw new DomainException(ex.ErrorData.Message);
            }
        }
    }
}
