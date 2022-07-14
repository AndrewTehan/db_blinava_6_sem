using System;
using Microsoft.Data.Sqlite;

namespace ConsoleApp1
{
    class Actions
    {
        public static void InitTables()
        {
            SqliteCommand command = new SqliteCommand(@"
                CREATE TABLE Countries (
                    idCountry integer primary key AUTOINCREMENT,
                    name text
                );

                CREATE TABLE ToursTypes (
                    idType integer primary key AUTOINCREMENT,
                    name text
                );

                CREATE TABLE Tours (
                    idTour integer primary key AUTOINCREMENT,
                    name text,
                    route text,
                    duration integer,
                    idCountry integer,
                    idType integer,
                    FOREIGN KEY (idCountry) REFERENCES Countries(idCountry),
                    FOREIGN KEY (idType) REFERENCES ToursTypes(idType)
                );

                PRAGMA foreign_keys = ON;
                ",
                Connection.SqliteConnection);
            command.ExecuteNonQuery();
            Console.WriteLine("Tables created!");
        }

        public static void AddTrigger()
        {
            SqliteCommand command = new SqliteCommand(@"
                create trigger if not exists trig
                after insert on Tours
                for each row
                begin
                    INSERT into Tours(name, route, duration, idCountry, idType)
                    VALUES ('trigger', 'routeNew', 20, 1, 1);
                end;
                ",
                Connection.SqliteConnection);
            command.ExecuteNonQuery();
            Console.WriteLine("Trigger added!");
        }

        public static void AddInitData()
        {
            SqliteCommand command = new SqliteCommand(@"
                INSERT into ToursTypes(name)
                VALUES ('TourType1');

                INSERT into ToursTypes(name)
                VALUES ('TourType2');

                INSERT into Countries(name)
                VALUES ('Countries1');

                INSERT into Countries(name)
                VALUES ('Countries2');
            ",
            Connection.SqliteConnection);

            command.ExecuteNonQuery();
        }

        public static void GetTours()
        {
            SqliteCommand command = new SqliteCommand("select * from Tours", Connection.SqliteConnection);
            using SqliteDataReader reader = command.ExecuteReader();
            Console.WriteLine("idTour,  name,  route,  duration,  idCountry,  idType");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["idTour"]} - {reader["name"]} - {reader["route"]} - {reader["duration"]} - {reader["idCountry"]} - {reader["idType"]}");
            }
        }

        public static void GetCountries()
        {
            SqliteCommand command = new SqliteCommand("select * from Countries", Connection.SqliteConnection);
            using SqliteDataReader reader = command.ExecuteReader();
            Console.WriteLine("idCountry,  name");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["idCountry"]} - {reader["name"]}");
            }
        }

        public static void GetTypes()
        {
            SqliteCommand command = new SqliteCommand("select * from ToursTypes", Connection.SqliteConnection);
            using SqliteDataReader reader = command.ExecuteReader();
            Console.WriteLine("idType,  name");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["idType"]} - {reader["name"]}");
            }
        }

        public static void AddTour(string name, string route, int duration, int idCountry, int idType)
        {
            SqliteCommand command = new SqliteCommand($@"
                INSERT into Tours(name, route, duration, idCountry, idType)
                VALUES (@name, @route, @duration, @idCountry, @idType)
            ",
            Connection.SqliteConnection);

            command.Parameters.Add(new SqliteParameter("@name", name));
            command.Parameters.Add(new SqliteParameter("@route", route));
            command.Parameters.Add(new SqliteParameter("@duration", duration));
            command.Parameters.Add(new SqliteParameter("@idCountry", idCountry));
            command.Parameters.Add(new SqliteParameter("@idType", idType));
            command.ExecuteNonQuery();
            Console.WriteLine("Tour Added!");
        }

        public static void UpdateTour(int id, string name, string route, int duration, int idCountry, int idType)
        {
            SqliteCommand command = new SqliteCommand(@"
                UPDATE Tours set name = @name, route = @route, duration = @duration, idType = @idType, idCountry = @idCountry where idTour = @id", Connection.SqliteConnection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.Parameters.Add(new SqliteParameter("@name", name));
            command.Parameters.Add(new SqliteParameter("@route", route));
            command.Parameters.Add(new SqliteParameter("@duration", duration));
            command.Parameters.Add(new SqliteParameter("@idCountry", idCountry));
            command.Parameters.Add(new SqliteParameter("@idType", idType));
            command.ExecuteNonQuery();
            Console.WriteLine($"Tour with id = {id} Updated!");
        }

        public static void DeleteTour(int id)
        {
            SqliteCommand command = new SqliteCommand("DELETE from Tours where idTour = @id", Connection.SqliteConnection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.ExecuteNonQuery();
            Console.WriteLine("Tour with id = {id} Deleted!");
        }
    }
}
