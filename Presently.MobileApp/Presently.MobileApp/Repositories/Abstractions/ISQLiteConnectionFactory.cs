using SQLite;

namespace Presently.MobileApp.Repositories.Abstractions
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection(string dbName);
    }
}
