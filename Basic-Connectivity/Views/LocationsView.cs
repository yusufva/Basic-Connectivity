using Basic_Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Views
{
    public  class LocationsView : GeneralView
    {
        public Locations InsertInput()
        {
            Console.Write("Input Locations Id (number): ");
            int locationsId;
            int.TryParse(Console.ReadLine(), out locationsId);
            Console.Write("Input Street Address: ");
            var locationStreet = Console.ReadLine();
            Console.Write("Input Postal Code: ");
            var locationPostal = Console.ReadLine();
            Console.Write("Input City: ");
            var locationCity = Console.ReadLine();
            Console.Write("Input State Province: ");
            var locationProvince = Console.ReadLine();
            Console.Write("Input Country Id (2 character): ");
            var locationCountryId = Console.ReadLine();


            return new Locations { 
                Id = locationsId, 
                streetAddress = locationStreet, 
                postalCode = locationPostal, 
                city = locationCity, 
                statProvince = locationProvince, 
                countryId = locationCountryId
            };
        }

        public Locations DeleteInput()
        {
            Console.Write("Input Locations Id (number): ");
            int locationsId;
            int.TryParse(Console.ReadLine(), out locationsId);

            return new Locations { Id = locationsId };
        }

        public Locations UpdateLocations()
        {
            Console.Write("Input Locations Id (number): ");
            int locationsId;
            int.TryParse(Console.ReadLine(), out locationsId);
            Console.Write("Input Street Address: ");
            var locationStreet = Console.ReadLine();
            Console.Write("Input Postal Code: ");
            var locationPostal = Console.ReadLine();
            Console.Write("Input City: ");
            var locationCity = Console.ReadLine();
            Console.Write("Input State Province: ");
            var locationProvince = Console.ReadLine();
            Console.Write("Input Country Id (2 character): ");
            var locationCountryId = Console.ReadLine();

            return new Locations
            {
                Id = locationsId,
                streetAddress = locationStreet,
                postalCode = locationPostal,
                city = locationCity,
                statProvince = locationProvince,
                countryId = locationCountryId
            };
        }
    }
}
