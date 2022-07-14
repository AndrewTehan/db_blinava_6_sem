using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity;

namespace ConsoleApp1
{
    internal class Connection : DbContext
    {
        public Connection() : base("testEntities") { }

        public DbSet<region> Regions { get; set; }
        public DbSet<world_cities> Cities { get; set; }
        public DbSet<world_countries> Countries { get; set; }

    }
}
