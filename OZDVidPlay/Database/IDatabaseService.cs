using SQLite.Net;

namespace OZDVidPlay
{
    public interface IDatabaseService
    {
        SQLiteConnection CreateConnection();
    }
}