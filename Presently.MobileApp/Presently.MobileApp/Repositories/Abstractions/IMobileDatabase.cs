using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Presently.MobileApp.Repositories.Abstractions
{
    public interface IMobileDatabase
    {
        void DeleteAll();
        void BulkInsert<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new();
        int Count<T>() where T : class, IDataObjectBase, new();
        int Count<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new();
        int DeleteAll<T>() where T : IDataObjectBase, new();
        int DeleteSingle<T>(long id) where T : IDataObjectBase, new();
        T FirstOrDefault<T>() where T : class, IDataObjectBase, new();
        T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new();
        IEnumerable<T> GetItemList<T>() where T : class, IDataObjectBase, new();
        long SaveItem<T>(T item) where T : class, IDataObjectBase, new();
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new();
    }
}
