using Presently.Common.DataContracts;
using System.Collections.Generic;

namespace Presently.FunctionApp.Services.Abstractions
{
    public interface IEmployeeSiteService
    {
        List<EmployeeSiteContract> GetSites(long employeeRecordId);
    }
}