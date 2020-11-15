using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.Base;
using Presently.MobileApp.Repositories.DataObjects;

namespace Presently.MobileApp.Repositories
{
    public class AttendanceLogRepository : Repository<AttendanceLogDataObject>, IAttendanceLogRepository
    {
        public AttendanceLogRepository(IMobileDatabase db) : base(db)
        {

        }
    }
}
