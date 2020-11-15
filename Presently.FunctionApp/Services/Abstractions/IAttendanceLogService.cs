using Presently.Common.DataContracts;
using Presently.Common.DataContracts.Requests;
using System.Collections.Generic;

namespace Presently.FunctionApp.Services.Abstractions
{
    public interface IAttendanceLogService
    {
        long? AddAttendanceLog(long employeeRecordId, AttendanceLogCreateRequestContract reqContract);
        AttendanceLogContract GetAttendanceLog(long recordId, long employeeRecordId);
        List<AttendanceLogContract> GetAttendanceLogs(long employeeRecordId);
    }
}