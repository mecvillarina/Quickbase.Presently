using Microsoft.Extensions.Options;
using Presently.Common.DataContracts;
using Presently.Common.DataContracts.Requests;
using Presently.Common.Models;
using Presently.FunctionApp.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presently.FunctionApp.Services
{
    public class AttendanceLogService : ServiceBase, IAttendanceLogService
    {
        private readonly string _dbId = "bqzfak5d2";

        public AttendanceLogService(IOptions<AppSettings> optionAppSettings) : base(optionAppSettings)
        {
        }

        public long? AddAttendanceLog(long employeeRecordId, AttendanceLogCreateRequestContract reqContract)
        {
            var row = new Dictionary<string, Datum>();
            row.Add("6", new Datum() { Value = reqContract.LocationName });
            row.Add("7", new Datum() { Value = reqContract.Latitude });
            row.Add("8", new Datum() { Value = reqContract.Longitude });
            //row.Add("9", new Datum() { Value = DateTime.UtcNow.ToString() });
            row.Add("10", new Datum() { Value = reqContract.LogType });
            row.Add("11", new Datum() { Value = employeeRecordId });

            var postRequest = new PostRequestModel(_dbId);
            postRequest.FieldsToReturn = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            postRequest.Data.Add(row);

            var result = PostRequest<PostRequestModel, PostResponseModel>("/v1/records", postRequest);

            if (result.Data.Any())
            {
                return (long)result.Data.First()["3"].Value;
            }

            return null;
        }

        public List<AttendanceLogContract> GetAttendanceLogs(long employeeRecordId)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            queryRequest.Where = $"{{11.EX.{employeeRecordId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);

            var logs = new List<AttendanceLogContract>();
            if (result.Data != null)
            {
                foreach (var data in result.Data)
                {
                    logs.Add(new AttendanceLogContract(data));
                }
            }

            return logs;
        }

        public AttendanceLogContract GetAttendanceLog(long recordId, long employeeRecordId)
        {
            var queryRequest = new QueryRequestModel(_dbId);
            queryRequest.Select = new List<long>() { 3, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            queryRequest.Where = $"{{3.EX.{recordId}}}AND{{11.EX.{employeeRecordId}}}";
            queryRequest.GroupBy = new List<QueryRequestGroupBy>() { new QueryRequestGroupBy() { FieldId = 3, Grouping = "equal-values" } };

            var result = PostRequest<QueryRequestModel, QueryResponseModel>("/v1/records/query", queryRequest);
            if (result.Data != null)
            {
                var data = result.Data.First();
                return new AttendanceLogContract(data);
            }

            return null;
        }
    }
}
