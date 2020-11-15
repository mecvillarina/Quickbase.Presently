using Presently.Common.DataContracts;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.WebServices.Abstractions;
using Presently.MobileApp.WebServices.Base;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices
{
    public class LocationWebService : WebServiceBase, ILocationWebService
    {
        public LocationWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<LocationAddressContract> AcquireAddress(double latitude, double longitude, string accessToken)
        {
            string endpoint = string.Format(ServerEndpoint.LocationAcquireAddress, latitude, longitude);
            return GetAsync<LocationAddressContract>(endpoint, null, accessToken);
        }
    }
}
