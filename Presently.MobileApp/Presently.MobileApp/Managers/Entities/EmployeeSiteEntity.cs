using Presently.Common.Models;
using System;
using System.Collections.Generic;

namespace Presently.MobileApp.Managers.Entities
{
    public class EmployeeSiteEntity
    {
        public long RecordId { get; set; }
        public string SiteName { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
        public double SiteRadius { get; set; }

    }
}
