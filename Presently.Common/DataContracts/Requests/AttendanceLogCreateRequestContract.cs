using System;
using System.Collections.Generic;
using System.Text;

namespace Presently.Common.DataContracts.Requests
{
    public class AttendanceLogCreateRequestContract
    {
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTimeOffset LogTime { get; set; }
        public string LogType { get; set; }
    }
}
