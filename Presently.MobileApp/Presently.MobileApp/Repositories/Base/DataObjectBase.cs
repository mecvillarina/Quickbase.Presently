using Presently.MobileApp.Repositories.Abstractions;
using SQLite;

namespace Presently.MobileApp.Repositories.Base
{
    public abstract class DataObjectBase : IDataObjectBase
    {
        [PrimaryKey, AutoIncrement]
        public virtual long RowId { get; set; }
    }
}
