using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    class City : SqlCrud
    {
        public City(Db db)
        {
            _db = db;
        }

        public override bool Delete(int id)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"delete from City where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_deleted = changedRows == 1;
            string message = is_deleted ? "City удалена" : "City не удалена";
            Console.WriteLine(message);
            return is_deleted;
        }

        public override void GetAll()
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from City",
                Connection = _db.Connection
            };

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string columnName1 = reader.GetName(0);
                string columnName2 = reader.GetName(1);

                Console.WriteLine($"{columnName1}\t {columnName2}\t");

                while (reader.Read())
                {
                    object id = reader.GetValue(0);
                    object city = reader.GetValue(1);                    

                    Console.WriteLine($"\t{id}: \t{city} ...");
                }
            }

            reader.Close();
            _db.closeConnection();

            Console.WriteLine($"------------------------------------");
        }

        public bool Insert(string city)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"insert into City(city)" +
                               $"values ('{city}')",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_inserted = changedRows == 1;
            string message = changedRows == 1 ? "City добавлена" : "City не добавлена";
            Console.WriteLine(message);
            return is_inserted;
        }

        public bool Update(int id, string city)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"update City set city = '{city}' where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_updated = changedRows == 1;
            string message = is_updated ? "City обновлена" : "City не обновлена";
            Console.WriteLine(message);
            return is_updated;
        }

        public override int LastId()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "select top 1 * from City order by id desc",
                Connection = _db.Connection
            };

            int res = (int)command.ExecuteScalar();

            return res;
        }
    }
}
