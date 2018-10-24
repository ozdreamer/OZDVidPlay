using System;
using System.IO;
using OZDVidPlay.iOS;
using Foundation;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace OZDVidPlay.iOS
{
    public class DatabaseService : IDatabaseService
    {
        public SQLiteConnection CreateConnection()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string path = Path.Combine(libFolder, $"{DatabaseManager.DatabaseFileName}.{DatabaseManager.DatabaseFileExtension}");

            // This is where we copy in the pre-created database
            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource(DatabaseManager.DatabaseFileName, DatabaseManager.DatabaseFileExtension);
                File.Copy(existingDb, path);
            }

            return new SQLiteConnection(new SQLitePlatformIOS(), path);
        }
    }
}