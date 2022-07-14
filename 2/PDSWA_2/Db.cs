using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    class Db
    {
        private string connection_string;
        private SqlConnection connection;

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public Db(string connection_string)
        {
            this.connection_string = connection_string;
            this.connection = new SqlConnection(connection_string);
        }

        public bool openConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                Console.WriteLine("Подключение открыто");
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Подключение закрыто...");
            }
            else
            {
                Console.WriteLine("Подключение не открыто...");
            }
        }
    }
}
