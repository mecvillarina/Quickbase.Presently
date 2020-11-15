using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Presently.MobileApp.Repositories.Abstractions
{
    public interface IRepository<T> where T : class, IDataObjectBase, new()
    {
        bool Any();
        int Count();
        int Count(Expression<Func<T, bool>> predicate);

        void Clear();
        int Remove(int id);

        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        IEnumerable<T> ToList();
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);

        long Add(T item);
        bool AddRange(IEnumerable<T> list);
    }
}
