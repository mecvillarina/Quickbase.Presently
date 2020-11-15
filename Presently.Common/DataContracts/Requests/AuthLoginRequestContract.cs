using Presently.Common.Abstractions;

namespace Presently.Common.DataContracts.Requests
{
    public class AuthLoginRequestContract : IJsonDataContract
    {
        public string EmployeeId { get; set; }
        public string AccessCode { get; set; }
    }
}
