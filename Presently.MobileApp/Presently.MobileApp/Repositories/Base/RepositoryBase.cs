using Presently.MobileApp.Repositories.Abstractions;

namespace Presently.MobileApp.Repositories.Base
{
    public class RepositoryBase
    {
        protected IMobileDatabase DB { get; }

        public RepositoryBase(IMobileDatabase db)
        {
            DB = db;
        }
    }
}
