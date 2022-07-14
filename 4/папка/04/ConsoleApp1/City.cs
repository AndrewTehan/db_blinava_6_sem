using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp1
{
    public partial class City : DbContext
    {
        public City()
            : base("name=City")
        {
        }

        public virtual DbSet<world_cities> world_cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
