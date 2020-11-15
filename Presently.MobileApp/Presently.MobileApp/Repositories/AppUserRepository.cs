using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.Base;
using Presently.MobileApp.Repositories.DataObjects;

namespace Presently.MobileApp.Repositories
{
    public class AppUserRepository : Repository<AppUserDataObject>, IAppUserRepository
    {
        public AppUserRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
