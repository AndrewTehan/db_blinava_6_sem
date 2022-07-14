using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    class Goods : SqlCrud
    {
        public Goods(Db db)
        {
            _db = db;
        }

        public override void GetAll()
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from Goods",
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
                    object shippingName = reader.GetValue(1);
                    object weightGoods = reader.GetValue(2);

                    Console.WriteLine($"\t{id}: \t{shippingName} \t{weightGoods} ...");
                }
            }

            reader.Close();
            _db.closeConnection();

            Console.WriteLine($"------------------------------------");
        }

        public bool Insert(string shippingName, int weightGoods)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"insert into Goods(shippingName, weightGoods)" +
                               $"values ('{shippingName}', {weightGoods})",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_inserted = changedRows == 1;
            string message = changedRows == 1 ? "Goods добавлена" : "Goods не добавлена";
            Console.WriteLine(message);
            return is_inserted;
        }

        public bool Update(int id, string shippingName)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"update Goods set shippingName = '{shippingName}' where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_updated = changedRows == 1;
            string message = is_updated ? "Goods обновлёна" : "Goods не обновлёна";
            Console.WriteLine(message);
            return is_updated;
        }

        public override bool Delete(int id)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"delete from Goods where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_deleted = changedRows == 1;
            string message = is_deleted ? "Goods удалёна" : "Goods не удалёна";
            Console.WriteLine(message);
            return is_deleted;
        }

        public override int LastId()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "select top 1 * from Goods order by id desc",
                Connection = _db.Connection
            };

            int res = (int)command.ExecuteScalar();

            return res;
        }
    }
}
