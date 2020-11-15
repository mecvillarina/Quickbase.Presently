using System;

namespace Presently.MobileApp.Managers.Entities
{
    public class AttendanceLogCreateRequestEntity
    {
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string LogType { get; set; }
    }
}
