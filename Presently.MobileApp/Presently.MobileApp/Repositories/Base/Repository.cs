using Presently.MobileApp.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Presently.MobileApp.Repositories.Base
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class, IDataObjectBase, new()
    {
        public Repository(IMobileDatabase db) : base(db)
        {
        }

        public bool Any()
        {
            return DB.Count<T>() > 0;
        }

        public int Count()
        {
            return DB.Count<T>();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return DB.Count<T>(predicate);
        }

        public virtual void Clear()
        {
            DB.DeleteAll<T>();
        }

        public virtual int Remove(int id)
        {
            return DB.DeleteSingle<T>(id);
        }

        public virtual T FirstOrDefault()
        {
            var item = DB.FirstOrDefault<T>();
            return item;
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var item = DB.FirstOrDefault<T>(predicate);
            return item;
        }

        public virtual IEnumerable<T> ToList()
        {
            var list = DB.GetItemList<T>();
            return list;
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            var list = DB.Where<T>(predicate);
            return list;
        }

        public virtual long Add(T item)
        {
            return DB.SaveItem(item);
        }

        public virtual bool AddRange(IEnumerable<T> list)
        {
            if (list == null) return false;

            DB.BulkInsert(list);

            return true;
        }

    }
}
