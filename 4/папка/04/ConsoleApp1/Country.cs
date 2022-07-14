using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp1
{
    public partial class Country : DbContext
    {
        public Country()
            : base("name=Country")
        {
        }

        public virtual DbSet<world_countries> world_countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
