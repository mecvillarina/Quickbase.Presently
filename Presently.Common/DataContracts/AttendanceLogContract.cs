using Presently.Common.Abstractions;
using Presently.Common.Models;
using System;
using System.Collections.Generic;

namespace Presently.Common.DataContracts
{
    public class AttendanceLogContract : IJsonDataContract
    {
        public long RecordId { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTimeOffset LogTime { get; set; }
        public string LogType { get; set; }
        public string Status { get; set; }

        public AttendanceLogContract()
        {

        }

        public AttendanceLogContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            LocationName = data["6"].Value.ToString();
            Latitude = Convert.ToDouble(data["7"].Value);
            Longitude = Convert.ToDouble(data["8"].Value);

            LogTime = DateTimeOffset.Parse(data["9"].Value.ToString()); 
            LogType = data["10"].Value.ToString();
            Status = data["14"].Value.ToString();
        }
    }
}
