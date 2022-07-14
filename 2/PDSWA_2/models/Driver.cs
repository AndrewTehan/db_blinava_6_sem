using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    class Driver : SqlCrud
    {
        public Driver(Db db)
        {
            _db = db;
        }

        public override bool Delete(int id)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"delete from Driver where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_deleted = changedRows == 1;
            string message = is_deleted ? "Пользоавтель удалён" : "Пользоавтель не удалён";
            Console.WriteLine(message);
            return is_deleted;
        }

        public override void GetAll()
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from Driver",
                Connection = _db.Connection
            };

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string columnName1 = reader.GetName(0);
                string columnName2 = reader.GetName(1);
                string columnName3 = reader.GetName(2);
                string columnName4 = reader.GetName(3);
                string columnName5 = reader.GetName(4);
                string columnName6 = reader.GetName(5);

                Console.WriteLine($"{columnName1}\t {columnName2}\t {columnName3}\t {columnName4}\t {columnName5}\t {columnName6}");

                while (reader.Read())
                {
                    object id = reader.GetValue(0);
                    object lastname = reader.GetValue(1);
                    object firstName = reader.GetValue(2);

                    Console.WriteLine($"\t{id}: \t{lastname} \t{firstName} ...");
                }
            }

            reader.Close();
            _db.closeConnection();

            Console.WriteLine($"------------------------------------");
        }

        public bool Insert(string lastName, string firstName, int DriverLicenseNumber, string category, int salary)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"insert into Driver(lastName, firstName, DriverLicenseNumber, category, salary)" +
                               $"values ('{lastName}', '{firstName}', {DriverLicenseNumber}, '{category}', {salary})",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_inserted = changedRows == 1;
            string message = changedRows == 1 ? "Водитель добавлен" : "Водитель не добавлен";
            Console.WriteLine(message);
            return is_inserted;
        }

        public bool Update(int id, string lastname)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"update Driver set lastname = '{lastname}' where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_updated = changedRows == 1;
            string message = is_updated ? "Водитель обновлён" : "Водитель не обновлён";
            Console.WriteLine(message);
            return is_updated;
        }

        public override int LastId()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "select top 1 * from Driver order by id desc",
                Connection = _db.Connection
            };

            int res = (int)command.ExecuteScalar();

            return res;
        }
    }
}
