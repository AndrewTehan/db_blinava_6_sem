using ConsoleApp1.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CityQ city = new CityQ();
            RegionQ region = new RegionQ();
            CountryQ country = new CountryQ();

            for(; ; )
            {
                Console.WriteLine("1-get all regions");
                Console.WriteLine("2-get city by id");
                Console.WriteLine("3-get country by id");
                Console.WriteLine("4-distance");
                Console.WriteLine("4-Contains");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    foreach (var item in region.GetRegions())
                    {
                        Console.WriteLine(item.publName + ' ' + item.id);
                    }
                }
                else if(choice == "2")
                {
                    Console.WriteLine("inter id city");
                    int id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(city.GetCity(id).name);
                }
                else if(choice == "3")
                {
                    Console.WriteLine("inter id country");
                    int id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(country.GetCountry(id).country);
                }
                else if(choice == "4")
                {
                    Console.WriteLine("inter id city1");
                    int id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("inter id city2");
                    int id2 = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("inter id city3");
                    int id3 = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(city.CheckDistance(id, id2, id3));
                }
                else if(choice == "5")
                {
                    Console.WriteLine("inter id country");
                    int id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("inter id city");
                    int id2 = Int32.Parse(Console.ReadLine());
                    country.CheckContains(id, id2);
                }
            }
        }
    }
}
