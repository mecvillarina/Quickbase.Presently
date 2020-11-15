using Presently.Common.DataContracts;
using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.WebServices.Abstractions;
using Presently.MobileApp.WebServices.Base;
using Presently.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices
{
    public class AttendanceLogWebService : WebServiceBase, IAttendanceLogWebService
    {
        public AttendanceLogWebService(IHttpService httpService, IHttpMessageHelper httpMessagepHelper) : base(httpService, httpMessagepHelper)
        {
        }

        public Task<AttendanceLogCollectionResponseDataContract> GetAll(string accessToken) => GetAsync<AttendanceLogCollectionResponseDataContract>(ServerEndpoint.AttendanceLogGetAll, null, accessToken);

        public Task<AttendanceLogContract> GetSingle(long recordId, string accessToken)
        {
            string endpoint = string.Format(ServerEndpoint.AttendanceLogGet, recordId);
            return GetAsync<AttendanceLogContract>(endpoint, null, accessToken);
        }

        public Task<AttendanceLogContract> Create(AttendanceLogCreateRequestContract reqContract, string accessToken) => PostAsync<AttendanceLogContract>(ServerEndpoint.AttendanceLogCreate, reqContract, null, accessToken);
    }
}
