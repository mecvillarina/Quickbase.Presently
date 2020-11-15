using System.Linq;

namespace Presently.MobileApp.Managers.Entities
{
    public class AppUserEntity
    {
        public long RecordId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; set; }
        public bool UseGeofencing { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";
        public string DisplayNameInitial => $"{FirstName.First()}{LastName.First()}";
    }
}
