using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Types;
using System.Net;
using System.Net.Mail;

namespace lab3
{
    public class StoredProcedure
    {
        [SqlProcedure]
        public static void AllGoods()
        {
            SqlCommand command = new SqlCommand();

            command.Connection = new SqlConnection("context connection = true");
            command.Connection.Open();

            string sql_string = "select * from Goods ;";

            command.CommandText = sql_string.ToString();
            SqlContext.Pipe.ExecuteAndSend(command);
            command.Connection.Close();
        }

        public static void send_email()
        {
            MailAddress from = new MailAddress("saloonofficial699@gmail.com", "Andrew");
            MailAddress to = new MailAddress("somemail@yandex.ru");

            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("saloonofficial699@gmail.com", "oubiztghxobglfwg");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        [SqlProcedure]
        public static void CityArea(SqlGeometry city)
        {
            SqlConnection Connection = new SqlConnection("context connection = true");
            Connection.Open();

            SqlContext.Pipe.Send(city.STArea().ToString());
            Connection.Close();
        }

        [SqlProcedure]
        public static void CityCoverage()
        {
            SqlCommand command = new SqlCommand { 
                CommandText = "select * from city ps inner join gadm40_blr_1 gb on ps.region = gb.ogr_fid",
                Connection = new SqlConnection("context connection = true")
            };

            command.Connection.Open();

            SqlContext.Pipe.ExecuteAndSend(command);

            command.Connection.Close();
        }
    }
}
