using Presently.MobileApp.Managers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presently.MobileApp.Managers.Abstractions
{
    public interface IEmployeeManager
    {
        Task<List<EmployeeSiteEntity>> GetSites();
        List<EmployeeSiteEntity> GetSitesLocally();
    }
}