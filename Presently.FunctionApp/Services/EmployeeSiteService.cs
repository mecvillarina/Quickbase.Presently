using Microsoft.Extensions.Options;
using Presently.Common.DataContracts;
using Presently.Common.Models;
using Presently.FunctionApp.Services.Abstractions;
using System.Collections.Generic;

namespace Presently.FunctionApp.Services
{
    public class EmployeeSiteService : ServiceBase, IEmployeeSiteService
    {
        private readonly string _dbId = "bqzfabsm4";

        public EmployeeSiteService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public List<EmployeeSiteContract> GetSites(long employeeRecordId)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11,12,13 };
            queryRequest.Where = $"{{8.EX.{employeeRecordId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var sites = new List<EmployeeSiteContract>();
            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    sites.Add(new EmployeeSiteContract(data));
                }
            }

            return sites;

        }
    }
}
