using Presently.Common.Abstractions;
using System;

namespace Presently.MobileApp.Managers.Entities
{
    public class AttendanceLogEntity
    {
        public long RecordId { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTimeOffset LogTime { get; set; }
        public string LogType { get; set; }
        public string Status { get; set; }

        public string LogTimeDisplay => LogTime.ToLocalTime().ToString("MM-dd-yyyy hh:mm tt");
    }
}
