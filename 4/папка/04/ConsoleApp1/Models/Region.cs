namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("region")]
    public partial class region
    {
        public int id { get; set; }

        [StringLength(255)]
        public string publName { get; set; }

        [Column("region")]
        public int? region1 { get; set; }
    }
}
