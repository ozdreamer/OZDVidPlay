using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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

        public List<Video> GetVideos(int playListId)
        {
            return this.dbConnection.Query<Video>($"SELECT * FROM Video WHERE PlayListId = {playListId}");
        }

        public Video AddVideo(Video video)
        {
            var videoId = this.dbConnection.InsertOrReplace(video);
            return this.GetVideo(videoId);
        }

        public Video GetVideo(int videoId)
        {
            return this.dbConnection.Query<Video>($"SELECT * FROM Video WHERE Id = {videoId}").FirstOrDefault();
        }

        public int NextVideoId => this.dbConnection.ExecuteScalar<int>("SELECT MAX(Id) FROM Video") + 1;

        public bool RemoveVideo(int videoId)
        {
            return this.dbConnection.Execute($"DELETE FROM Video WHERE Id = {videoId}") > 0;
        }

        public int NextVideoSequence(int playListId)
        {
            return this.dbConnection.ExecuteScalar<int>($"SELECT COALESCE(MAX(Sequence),1) FROM Video WHERE PlayListId = {playListId}");
        }
    }
}
