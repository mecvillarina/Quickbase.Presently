using Presently.MobileApp.Managers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presently.MobileApp.Managers.Abstractions
{
    public interface IAttendanceLogManager
    {
        Task<List<AttendanceLogEntity>> GetLogs();
        List<AttendanceLogEntity> GetLogsLocally();
        Task<AttendanceLogEntity> Get(long recordId);
        Task<AttendanceLogEntity> Create(AttendanceLogCreateRequestEntity reqEntity);
    }
}