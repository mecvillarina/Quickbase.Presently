using Presently.Common.Models;
using System;
using System.Collections.Generic;

namespace Presently.Common.DataContracts
{
    public class EmployeeSiteContract
    {
        public long RecordId { get; set; }
        public string SiteName { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
        public double SiteRadius { get; set; }

        public EmployeeSiteContract()
        {

        }

        public EmployeeSiteContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            SiteName = data["7"].Value.ToString();
            SiteLatitude = Convert.ToDouble(data["11"].Value);
            SiteLongitude = Convert.ToDouble(data["12"].Value);
            SiteRadius = Convert.ToDouble(data["13"].Value);
        }
    }
}
