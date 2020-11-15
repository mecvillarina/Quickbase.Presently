using Microsoft.Extensions.Options;
using Presently.Common.DataContracts;
using Presently.Common.Models;
using Presently.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Presently.FunctionApp.Services
{
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        private readonly string _dbId = "bqze9jzyr";

        public EmployeeService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public EmployeeContract GetEmployee(long recordId)
        {
            var query = new QueryRequestModel(_dbId);
            query.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12 };
            query.Where = $"{{3.EX.{recordId}}}";
            query.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", query);

            if (result.Data.Any())
            {
                var data = result.Data.First();
                return new EmployeeContract(data);
            }

            return null;
        }
    }
}
