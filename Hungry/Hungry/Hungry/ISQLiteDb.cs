using SQLite;

namespace Hungry
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

