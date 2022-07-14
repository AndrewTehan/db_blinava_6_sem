using Microsoft.Data.Sqlite;

namespace ConsoleApp1
{
    class Connection
    {
        private static Connection instance;
        public static SqliteConnection SqliteConnection { get; private set; }
        private Connection()
        {
            SqliteConnection = new SqliteConnection(@"Data Source=C:\Users\Andrew\Desktop\6_sem\БД\11\Lab11\travel.db");
            SqliteConnection.Open();
        }
        public static Connection getInstance()
        {
            if (instance == null)
                instance = new Connection();
            return instance;
        }
        public static void Close()
        {
            if (SqliteConnection.State == System.Data.ConnectionState.Open)
                SqliteConnection.Close();
        }
    }
}
