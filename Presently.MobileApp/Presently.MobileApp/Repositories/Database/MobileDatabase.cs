using Presently.MobileApp.Repositories.Abstractions;
using Presently.MobileApp.Repositories.DataObjects;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Presently.MobileApp.Repositories.Database
{
    public class MobileDatabase : IMobileDatabase
    {
        private const string DBName = "Presently.db3";
        private static readonly object _locker = new object();
        private readonly SQLiteConnection DB;

        public MobileDatabase(ISQLiteConnectionFactory connectionFactory)
        {
            lock (_locker)
            {
                DB = connectionFactory.CreateConnection(DBName);
                DB.CreateTable<AppUserDataObject>();
                DB.CreateTable<AttendanceLogDataObject>();
                DB.CreateTable<EmployeeSiteDataObject>();
            }
        }

        public void DeleteAll()
        {
            DB.DeleteAll<AppUserDataObject>();
            DB.DeleteAll<AttendanceLogDataObject>();
            DB.DeleteAll<EmployeeSiteDataObject>();
        }

        public void BulkInsert<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                DB.InsertAll(list, typeof(T));
            }
        }

        public int Count<T>() where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                return DB.Table<T>().Count();
            }
        }

        public int Count<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                return DB.Table<T>().Count(expression);
            }
        }

        public int DeleteAll<T>() where T : IDataObjectBase, new()
        {
            lock (_locker)
            {
                return DB.DeleteAll<T>();
            }
        }

        public int DeleteSingle<T>(long id) where T : IDataObjectBase, new()
        {
            lock (_locker)
            {
                return DB.Delete<T>(id);
            }
        }

        public T FirstOrDefault<T>() where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                var item = DB.Table<T>().FirstOrDefault();

                return item;
            }
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                var item = DB.Table<T>().FirstOrDefault(expression);

                return item;
            }
        }

        public T GetItem<T>(long id) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                var item = DB.Table<T>().FirstOrDefault(x => x.RowId == id);

                return item;
            }
        }

        public IEnumerable<T> GetItemList<T>() where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                IEnumerable<T> list = DB.Table<T>().ToList();
                return list;
            }
        }

        public long SaveItem<T>(T item) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                if (item.RowId != 0)
                {
                    DB.Update(item);
                    return item.RowId;
                }
                else
                {
                    DB.Insert(item);
                    var newID = DB.ExecuteScalar<long>("select last_insert_rowid();");
                    return newID;
                }
            }
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                var item = DB.Table<T>().Where(expression).ToList();

                return item;
            }
        }
    }
}
