using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Presently.Common.DataContracts.Requests;
using Presently.Common.DataContracts.Responses;
using Presently.FunctionApp.Providers;
using Presently.FunctionApp.Providers.Abstractions;
using Presently.FunctionApp.Services.Abstractions;

namespace Presently.FunctionApp.Apis
{
    public class AuthController : AuthorizeMobileControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IEmployeeService employeeService, IAccessTokenProvider accessTokenProvider, IAuthService authService) : base(employeeService, accessTokenProvider)
        {
            _authService = authService;
        }

        [FunctionName("AuthLogin")]
        public IActionResult Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/login")] AuthLoginRequestContract contract)
        {
            if (string.IsNullOrEmpty(contract.AccessCode) || string.IsNullOrEmpty(contract.EmployeeId))
            {
                return new BadRequestResult();
            }

            var id = _authService.Login(contract.EmployeeId, contract.AccessCode);

            if (id.HasValue)
            {
                var jwtResult = AccessTokenProvider.GenerateToken(id.ToString());
                return new OkObjectResult(jwtResult);
            }

            return new BadRequestObjectResult(new BadRequestResponseContract() { Message = "Invalid Username or password." });
        }

        [FunctionName("AuthGetProfile")]
        public IActionResult GetProfile([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "auth/me")] HttpRequest req)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);

            if (user != null)
            {
                return new OkObjectResult(user);
            }

            return new BadRequestResult();
        }
    }
}
