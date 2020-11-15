using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.WebServices.Abstractions;
using Presently.MobileApp.WebServices.Base;
using Presently.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices
{
    public class EmployeeWebService : WebServiceBase, IEmployeeWebService
    {
        public EmployeeWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<EmployeeSiteCollectionResponseDataContract> GetEmployeeSites(string accessToken) => GetAsync<EmployeeSiteCollectionResponseDataContract>(ServerEndpoint.EmployeeSites, null, accessToken);
    }
}
