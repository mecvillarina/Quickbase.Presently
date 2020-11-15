using Microsoft.AspNetCore.Http;
using Presently.Common.DataContracts;
using Presently.FunctionApp.Providers;
using Presently.FunctionApp.Providers.Abstractions;
using Presently.FunctionApp.Services.Abstractions;
using System.Linq;

namespace Presently.FunctionApp.Apis
{
    public class AuthorizeMobileControllerBase
    {
        public readonly IEmployeeService EmployeeService;
        public readonly IAccessTokenProvider AccessTokenProvider;

        public AuthorizeMobileControllerBase(IEmployeeService EmployeeService, IAccessTokenProvider accessTokenProvider)
        {
            this.EmployeeService = EmployeeService;
            AccessTokenProvider = accessTokenProvider;
        }

        public AccessTokenResult ValidateToken(HttpRequest req) => AccessTokenProvider.ValidateToken(req);

        public EmployeeContract GetUser(AccessTokenResult tokenResult)
        {
            if (tokenResult.Status == AccessTokenStatus.Valid)
            {
                var userId = long.Parse(tokenResult.Principal.Claims.First(x => x.Type == "id").Value);
                return EmployeeService.GetEmployee(userId);
            }

            return null;
        }
    }
}
