using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking.models
{
    class Able_type : SqlCrud
    {
        public Able_type(Db db)
        {
            _db = db;
        }

        public override void GetAll()
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {   
                CommandText = "select * from able_types",
                Connection = _db.Connection
            };

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string columnName1 = reader.GetName(0);

                Console.WriteLine($"{columnName1}");

                while (reader.Read())
                {
                    object name = reader.GetValue(0);
                    Console.WriteLine($"\t{name}");
                }
            }

            reader.Close();
            _db.closeConnection();

            Console.WriteLine($"------------------------------------");
        }

        public bool Insert(string name)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"insert into able_types(name)" +
                               $"values ('{name}')",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_inserted = changedRows == 1;
            string message = changedRows == 1 ? "Тип добавлен" : "Тип не добавлен";
            Console.WriteLine(message);
            return is_inserted;
        }

        public bool Update(string name)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"update able_types set name = '{name}'",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_updated = changedRows == 1;
            string message = is_updated ? " Тип обновлён" : "Тип не обновлён";
            Console.WriteLine(message);
            return is_updated;
        }

        public bool Delete_type(string name)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"delete from able_types where name = {name}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_deleted = changedRows == 1;
            string message = is_deleted ? "Тип удалён" : "Тип не удалён";
            Console.WriteLine(message);
            return is_deleted;
        }

        public override int LastId()
        {
            return -1;
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
