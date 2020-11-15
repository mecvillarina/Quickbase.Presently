using Presently.MobileApp.Repositories.Base;
using System;

namespace Presently.MobileApp.Repositories.DataObjects
{
    public class AppUserDataObject : DataObjectBase
    {
        public long Id { get; set; }
        public string LoginProvider { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateRegistered { get; set; }
        
        public long? BoatOperatorId { get; set; }
        public long? DiveProfessionalId { get; set; }
    }
}
