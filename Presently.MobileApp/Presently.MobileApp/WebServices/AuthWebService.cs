using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.WebServices.Abstractions;
using Presently.MobileApp.WebServices.Base;
using Presently.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices
{
    public class AuthWebService : WebServiceBase, IAuthWebService
    {
        public AuthWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<AuthTokenDataContract> Login(AuthLoginRequestContract contract) => PostAsync<AuthTokenDataContract>(ServerEndpoint.AuthLogin, contract);
    }
}
