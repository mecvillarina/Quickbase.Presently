using Presently.MobileApp.WebServices.DataContracts;
using System.Threading.Tasks;

namespace Presently.MobileApp.WebServices.Abstractions
{
    public interface IEmployeeWebService
    {
        Task<EmployeeSiteCollectionResponseDataContract> GetEmployeeSites(string accessToken);
    }
}