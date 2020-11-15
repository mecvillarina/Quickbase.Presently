using Presently.Common.Models;
using System;
using System.Collections.Generic;

namespace Presently.Common.DataContracts
{
    public class EmployeeContract
    {
        public long RecordId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; set; }
        public bool UseGeofencing { get; set; }

        public EmployeeContract()
        {

        }

        public EmployeeContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            FirstName = data["6"].Value.ToString();
            MiddleName = data["7"].Value.ToString();
            LastName = data["8"].Value.ToString();
            EmployeeID = data["9"].Value.ToString();
            UseGeofencing = Convert.ToBoolean(data["11"].Value);
        }
    }
}
