using Presently.Common.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Abstractions
{
    public interface ILocationWebService
    {
        Task<LocationAddressContract> AcquireAddress(double latitude, double longitude, string accessToken);
    }
}