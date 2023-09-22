using Basic_Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Views
{
    public class CountryView : GeneralView
    {
        public Country InsertInput()
        {
            Console.Write("Input Country Id (2 character): ");
            var countryId = Console.ReadLine();
            Console.Write("Input Country Name: ");
            var countryName = Console.ReadLine();
            Console.Write("Input Country Region Id(Number): ");
            int countryRegionId;
            int.TryParse(Console.ReadLine(), out countryRegionId);
            

            return new Country { Id = countryId, Name = countryName, RegionId = countryRegionId};
        }

        public Country DeleteInput()
        {
            Console.WriteLine("Insert region id");
            var id = Console.ReadLine();

            return new Country { Id = id};
        }

        public Country UpdateCountry()
        {
            Console.Write("Input Country Id (2 character): ");
            var countryId = Console.ReadLine();
            Console.Write("Input Country Name: ");
            var countryName = Console.ReadLine();
            Console.Write("Input Country Region Id(Number): ");
            int countryRegionId;
            int.TryParse(Console.ReadLine(), out countryRegionId);

            return new Country
            {
                Id = countryId,
                Name = countryName,
                RegionId = countryRegionId
            };
        }
    }
}
