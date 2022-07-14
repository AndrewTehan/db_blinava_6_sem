using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Queries
{
    internal class RegionQ
    {
        public Connection Connection { get; set; }

        public RegionQ()
        {
            Connection = new Connection();
        }

        public List<region> GetRegions()
        {
            List<region> regions = new List<region>();
            foreach (var item in Connection.Regions)
            {
                regions.Add(item);
            }
            return regions;
        }

        public void AddRegion(region region)
        {
            Connection.Regions.Add(region);
            Connection.SaveChanges();
        }
    }
}
