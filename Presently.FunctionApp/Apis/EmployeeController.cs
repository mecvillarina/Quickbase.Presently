using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Presently.FunctionApp.Providers;
using Presently.FunctionApp.Providers.Abstractions;
using Presently.FunctionApp.Services.Abstractions;

namespace Presently.FunctionApp.Apis
{
    public class EmployeeController : AuthorizeMobileControllerBase
    {
        private readonly IEmployeeSiteService _employeeSiteService;
        public EmployeeController(IEmployeeService EmployeeService, 
            IAccessTokenProvider accessTokenProvider,
            IEmployeeSiteService employeeSiteService) : base(EmployeeService, accessTokenProvider)
        {
            _employeeSiteService = employeeSiteService;
        }

        [FunctionName("EmployeeSites")]
        public IActionResult Query([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employee/sites")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);
            var sites = _employeeSiteService.GetSites(user.RecordId);
            return new OkObjectResult(sites);
        }
    }
}
