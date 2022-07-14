namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class world_countries
    {
        [Key]
        public int ogr_fid { get; set; }

        public DbGeometry ogr_geometry { get; set; }

        [StringLength(100)]
        public string country { get; set; }
    }
}
