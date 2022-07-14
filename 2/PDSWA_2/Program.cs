using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Trucking
{
    class Program
    {
        static void Main(string[] args)
        {
            string TransportParkServiceConnectionString = @"Data Source=LAPTOP-DCR90V30;Initial Catalog=Trucking;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Db db = new Db(TransportParkServiceConnectionString);

            ConsoleStep consoleStep = new ConsoleStep();
            consoleStep.Interaction(db);

            Console.WriteLine("Программа завершила работу.");
            Console.Read();
        }        
    }
}
