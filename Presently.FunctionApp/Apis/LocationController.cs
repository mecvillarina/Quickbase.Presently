using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Presently.Common.DataContracts;
using Presently.FunctionApp.Apis;
using Presently.FunctionApp.Providers;
using Presently.FunctionApp.Providers.Abstractions;
using Presently.FunctionApp.Services.Abstractions;

namespace Presently.FunctionApp.Apis
{
    public class LocationController : AuthorizeMobileControllerBase
    {
        private readonly ILocationIqService _locationIqService;

        public LocationController(IEmployeeService employeeService,
            IAccessTokenProvider accessTokenProvider,
            ILocationIqService locationIqService) : base(employeeService, accessTokenProvider)
        {
            _locationIqService = locationIqService;
        }

        [FunctionName("LocationAcquireAddress")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "location/acquire-address/{latitude:double}/{longitude:double}")] HttpRequest req, double latitude, double longitude, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var contract = new LocationAddressContract();
            contract.FormattedTextAddress = _locationIqService.GetFormattedAddress(latitude, longitude);
            return new OkObjectResult(contract);
        }
    }
}
