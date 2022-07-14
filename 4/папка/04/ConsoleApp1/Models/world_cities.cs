namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class world_cities
    {
        [Key]
        public int ogr_fid { get; set; }

        public DbGeometry ogr_geometry { get; set; }

        [StringLength(40)]
        public string name { get; set; }

        [StringLength(12)]
        public string country { get; set; }

        [StringLength(1)]
        public string capital { get; set; }
    }
}
