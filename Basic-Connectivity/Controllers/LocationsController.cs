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
                _locationsView.List(result, "regions");
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
                    if (string.IsNullOrEmpty(input.streetAddress))
                    {
                        Console.WriteLine("locations ID/Name cannot be empty!");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _locations.Insert(new locations
            {
                Id = input.Id,
                Name = input.Name,
                RegionId = input.RegionId
            });
            _locationsView.Message(result);
        }

        public void Update()
        {
            var locations = new locations();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    locations = _locationsView.Updatelocations();
                    if (string.IsNullOrEmpty(locations.Id) || string.IsNullOrEmpty(locations.Name))
                    {
                        Console.WriteLine("locations ID/Name cannot be empty");
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
            var locations = new locations();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    locations = _locationsView.DeleteInput();
                    var data = _locations.GetById(locations.Id);
                    if (string.IsNullOrEmpty(data.Name))
                    {
                        Console.WriteLine($"No Region with Id = {data.Id}");
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
