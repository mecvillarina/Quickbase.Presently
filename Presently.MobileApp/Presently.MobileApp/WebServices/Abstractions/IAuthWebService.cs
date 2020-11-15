using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Abstractions
{
    public interface IAuthWebService
    {
        Task<AuthTokenDataContract> Login(AuthLoginRequestContract contract);
    }
}