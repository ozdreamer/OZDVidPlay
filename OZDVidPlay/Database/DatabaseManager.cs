using System;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace OZDVidPlay
{
    public class DatabaseManager
    {
        public const string DatabaseFileName = "OZDVidPlayList";

        public const string DatabaseFileExtension = "db";

        SQLiteConnection dbConnection;

        public DatabaseManager()
        {
            this.dbConnection = DependencyService.Get<IDatabaseService>().CreateConnection();
        }

        public List<PlayList> GetAllPlayLists()
        {
            return this.dbConnection.Query<PlayList>("SELECT * FROM PlayList");
        }
    }
}
