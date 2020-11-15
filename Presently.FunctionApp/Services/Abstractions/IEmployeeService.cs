using Presently.Common.DataContracts;

namespace Presently.FunctionApp.Services.Abstractions
{
    public interface IEmployeeService
    {
        EmployeeContract GetEmployee(long recordId);
    }
}