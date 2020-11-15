using Microsoft.Extensions.Options;
using Presently.Common.Models;
using Presently.FunctionApp.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Presently.FunctionApp.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly string _dbId = "bqze9jzyr";

        public AuthService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public long? Login(string employeeId, string accessCode)
        {
            var query = new QueryRequestModel(_dbId);
            query.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12 };
            query.Where = $"{{9.EX.'{employeeId}'}}AND{{10.EX.'{accessCode}'}}";
            query.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", query);

            if (result.Data.Any())
            {
                return (long)result.Data.First()["3"].Value;
            }

            return null;
        }
    }
}
