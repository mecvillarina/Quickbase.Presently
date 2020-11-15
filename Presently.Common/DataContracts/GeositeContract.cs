using Presently.Common.Models;
using System;
using System.Collections.Generic;

namespace Presently.Common.DataContracts
{
    public class GeositeContract
    {
        public long RecordId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }

        public GeositeContract()
        {

        }

        public GeositeContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            Name= data["6"].Value.ToString();
            Latitude = Convert.ToDouble(data["7"].Value);
            Longitude = Convert.ToDouble(data["8"].Value);
            Radius = Convert.ToDouble(data["9"].Value);
        }
    }
}
