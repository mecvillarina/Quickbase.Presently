using Presently.Common.DataContracts;
using Presently.Common.DataContracts.Requests;
using Presently.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Abstractions
{
    public interface IAttendanceLogWebService
    {
        Task<AttendanceLogCollectionResponseDataContract> GetAll(string accessToken);
        Task<AttendanceLogContract> GetSingle(long recordId, string accessToken);
        Task<AttendanceLogContract> Create(AttendanceLogCreateRequestContract reqContract, string accessToken);
    }
}