using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presently.Common.DataContracts.Requests;
using Presently.Common.DataContracts.Responses;
using Presently.FunctionApp.Providers;
using Presently.FunctionApp.Providers.Abstractions;
using Presently.FunctionApp.Services.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Presently.FunctionApp.Apis
{
    public class AttendanceLogController : AuthorizeMobileControllerBase
    {
        private readonly IAttendanceLogService _attendanceLogService;
        public AttendanceLogController(IEmployeeService EmployeeService,
            IAccessTokenProvider accessTokenProvider,
            IAttendanceLogService attendanceLogService) : base(EmployeeService, accessTokenProvider)
        {
            _attendanceLogService = attendanceLogService;
        }


        [FunctionName("AttendanceLogGetAll")]
        public IActionResult GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "attendance-log/get-all")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);
            var purchaseOrders = _attendanceLogService.GetAttendanceLogs(user.RecordId);
            return new OkObjectResult(purchaseOrders);
        }

        [FunctionName("AttendanceLogGet")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "attendance-log")] HttpRequest req, ILogger logger)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);
            var query = req.Query["recordId"].ToString();

            if (!string.IsNullOrEmpty(query))
            {
                var recordId = Convert.ToInt64(query);
                var purchaseOrder = _attendanceLogService.GetAttendanceLog(recordId, user.RecordId);

                if (purchaseOrder != null)
                {
                    return new OkObjectResult(purchaseOrder);
                }
            }

            return new BadRequestResult();
        }

        [FunctionName("AttendanceLogCreate")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "attendance-log/create")] HttpRequest req)
        {
            var tokenResult = ValidateToken(req);
            if (tokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var user = GetUser(tokenResult);
            using (var streamReader = new StreamReader(req.Body))
            {
                string requestBody = await streamReader.ReadToEndAsync();
                var contract = JsonConvert.DeserializeObject<AttendanceLogCreateRequestContract>(requestBody);

                var id = _attendanceLogService.AddAttendanceLog(user.RecordId, contract);

                if (id.HasValue)
                {
                    var attendanceLog = _attendanceLogService.GetAttendanceLog(id.Value, user.RecordId);
                    return new OkObjectResult(attendanceLog);
                }

                return new BadRequestObjectResult(new BadRequestResponseContract() { Message = "Customer was not created" });
            }
        }
    }
}
