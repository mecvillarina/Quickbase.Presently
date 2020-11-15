using Presently.MobileApp.Repositories.Base;

namespace Presently.MobileApp.Repositories.DataObjects
{
    public class AppUserDataObject : DataObjectBase
    {
        public long RecordId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; set; }
        public bool UseGeofencing { get; set; }
    }
}
