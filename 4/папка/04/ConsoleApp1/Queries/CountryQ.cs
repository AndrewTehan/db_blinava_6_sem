using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using Microsoft.SqlServer.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Queries
{
    internal class CountryQ
    {
        public Connection Connection { get; set; }

        public CountryQ()
        {
            Connection = new Connection();
        }

        public world_countries GetCountry(int id)
        {
            return Connection.Countries.Where(c => c.ogr_fid == id).SingleOrDefault();
        }

        public void CheckContains(int idCountry, int idCity)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            string sqlconnection = ConfigurationManager.ConnectionStrings["testEntities"].ConnectionString;
            SqlConnection connection = new SqlConnection(sqlconnection);
            connection.Open();
            SqlCommand command = new SqlCommand("checkContains", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            System.Byte resul = 0;
            var res = new SqlParameter("@result", resul);
            res.Direction = System.Data.ParameterDirection.Output;
            command.Parameters.AddWithValue("@country", idCountry);
            command.Parameters.AddWithValue("@city", idCity);
            command.Parameters.Add(res);
            var reader = command.ExecuteNonQuery();
            var result = res.Value.ToString();
            Console.WriteLine(result);
            connection.Close();
        }
    }
}
