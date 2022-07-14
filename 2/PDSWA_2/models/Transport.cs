using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    class Transport : SqlCrud
    {
        public Transport(Db db)
        {
            _db = db;
        }

        public override void GetAll()
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from Transport",
                Connection = _db.Connection
            };

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string columnName1 = reader.GetName(0);
                string columnName2 = reader.GetName(1);
                string columnName3 = reader.GetName(2);

                Console.WriteLine($"{columnName1}\t {columnName2}\t {columnName3}");

                while (reader.Read())
                {
                    object transportNumber = reader.GetValue(0);
                    object capacity = reader.GetValue(1);
                    object idDriver = reader.GetValue(2);

                    Console.WriteLine($"\t{transportNumber}: \t{capacity}, \t{idDriver} ...");
                }
            }

            reader.Close();
            _db.closeConnection();

            Console.WriteLine($"------------------------------------");
        }

        public bool Insert(string transportNumber, int capacity, int idDriver)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"insert into Transport(transportNumber, capacity, idDriver)" +
                               $"values ('{transportNumber}', {capacity}, {idDriver})",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_inserted = changedRows == 1;
            string message = changedRows == 1 ? "Transport добавлена" : "Transport не добавлена";
            Console.WriteLine(message);
            return is_inserted;
        }

        public bool Update(int id, int capacity)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"update Transport set capacity = '{capacity}' where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_updated = changedRows == 1;
            string message = is_updated ? "Transport обновлён" : "Transport не обновлён";
            Console.WriteLine(message);
            return is_updated;
        }

        public override bool Delete(int id)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"delete from Transport where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_deleted = changedRows == 1;
            string message = is_deleted ? "Transport удалён" : "Transport не удалён";
            Console.WriteLine(message);
            return is_deleted;
        }

        public override int LastId()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "select top 1 * from  Transport order by id desc",
                Connection = _db.Connection
            };

            int res = (int)command.ExecuteScalar();

            return res;
        }
    }
}
