using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Shapes;
using Path = System.IO.Path;

namespace MovieApp
{

    internal class Database
    {
        //Check if the system it's running is android 
        static bool IsAndroid() => DeviceInfo.Platform == DevicePlatform.Android;

        // Initiate a private database connection
        static SQLiteConnection newConnection()
        {
            string libraryPath;
            var path = "";
            var sqliteFilename = "Film.db3";

            //if __ANDROID__
            if (IsAndroid())
            {
                // Just use whatever directory SpecialFolder.Personal returns
                libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                path = Path.Combine(libraryPath, sqliteFilename);
            }
            else if(!IsAndroid())
            {
                //else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); 
                // Documents folder
                libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
                path = Path.Combine(libraryPath, sqliteFilename);
            }


            // Create Sqlite database connection
            //SQLiteConnection connection = new SQLiteConnection("Data Source = ./film.db; Version = 3;");
            var connection = new SQLiteConnection(path);

            Console.WriteLine(path);

            return connection;
        }

        // Create a new movie table in the database
        static void createTable()
        {
            newConnection().CreateTable<Film>();
        }

        // Insert data in the database
        static void insertDb(Film film)
        {
            newConnection().Insert(film);
        }

        static List<Film> readTable()
        {
            List<Film> films = new List<Film>();
            SQLiteConnection connection = newConnection();
            
            //Film film = connection.Get<Film>(index);
            var listFilms = connection.Table<Film>();

            foreach(var data in listFilms)
            {
                films.Add(data);
            }

            return films;
        }


        // get database connection
        public static SQLiteConnection getDbConnection() => newConnection();

        // get create table command
        public static void createDbTable() => createTable();

        // get update table command
        public static void updateDatabase( Film film) => insertDb(film);

        //get read table command
        public static List<Film> readDbTable() => readTable();
    }
}
