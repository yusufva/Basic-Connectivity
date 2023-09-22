using Basic_Connectivity.Models;
using Basic_Connectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Controllers
{
    internal class LocationsController
    {
        private Locations _locations;
        private LocationsView _locationsView;

        public LocationsController(Locations locations, LocationsView locationsView)
        {
            _locations = locations;
            _locationsView = locationsView;
        }

        public void GetAll()
        {
            var result = _locations.GetAll();
            if (!result.Any())
            {
                Console.WriteLine("No record found");
            }
            {
                _locationsView.List(result, "locations");
            }
        }

        public void Insert()
        {
            var input = new Locations();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    input = _locationsView.InsertInput();
                    if (string.IsNullOrEmpty(input.city))
                    {
                        Console.WriteLine("locations city cannot be empty!");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _locations.Insert(new Locations
            {
                Id = input.Id,
                streetAddress = input.streetAddress,
                postalCode = input.postalCode,
                city = input.city,
                statProvince = input.statProvince,
                countryId = input.countryId
            });
            _locationsView.Message(result);
        }

        public void Update()
        {
            var locations = new Locations();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    locations = _locationsView.Updatelocations();
                    if (string.IsNullOrEmpty(locations.city))
                    {
                        Console.WriteLine("locations city cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _locations.Update(locations);
            _locationsView.Message(result);
        }

        public void Delete()
        {
            var locations = new Locations();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    locations = _locationsView.DeleteInput();
                    var data = _locations.GetById(locations.Id);
                    if (string.IsNullOrEmpty(data.city))
                    {
                        Console.WriteLine($"No Locations with Id = {data.Id}");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _locations.Delete(locations.Id);
            _locationsView.Message(result);
        }
    }
}
