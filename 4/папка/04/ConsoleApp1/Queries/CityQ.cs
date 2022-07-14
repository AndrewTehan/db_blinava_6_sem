using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Queries
{
    internal class CityQ
    {
        public Connection Connection { get; set; }

        public CityQ()
        {
            Connection = new Connection();
        }

        public world_cities GetCity(int id)
        {
            return Connection.Cities.Where(c => c.ogr_fid == id).SingleOrDefault();
        }

        public float CheckDistance(int city1, int city2, int city3)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            var cityA = GetCity(city1).ogr_geometry;
            var cityB = GetCity(city2).ogr_geometry;
            var cityC = GetCity(city3).ogr_geometry;
            SqlGeometry acity = SqlGeometry.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(cityA.AsBinary()), cityA.CoordinateSystemId);
            SqlGeometry bcity = SqlGeometry.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(cityB.AsBinary()), cityB.CoordinateSystemId);
            SqlGeometry ccity = SqlGeometry.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(cityC.AsBinary()), cityC.CoordinateSystemId);
            if (acity.STDistance(bcity) < acity.STDistance(ccity)) return (float)acity.STDistance(bcity);
            else return (float)acity.STDistance(ccity);
        }
    }
}
