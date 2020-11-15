using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.Base;
using Presently.MobileApp.Repositories.DataObjects;

namespace Presently.MobileApp.Repositories
{
    public class EmployeeSiteRepository : Repository<EmployeeSiteDataObject>, IEmployeeSiteRepository
    {
        public EmployeeSiteRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
