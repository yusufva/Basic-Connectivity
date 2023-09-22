using Basic_Connectivity.Models;
using Basic_Connectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Controllers
{
    public class CountryController
    {
        private Country _country;
        private CountryView _countryView;

        public CountryController(Country country, CountryView countryView)
        {
            _country = country;
            _countryView = countryView;
        }

        public void GetAll()
        {
            var result = _country.GetAll();
            if (!result.Any())
            {
                Console.WriteLine("No record found");
            }
            {
                _countryView.List(result, "country");
            }
        }

        public void Insert()
        {
            var input = new Country();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    input = _countryView.InsertInput();
                    if (string.IsNullOrEmpty(input.Id) || string.IsNullOrEmpty(input.Name))
                    {
                        Console.WriteLine("Country ID/Name cannot be empty!");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _country.Insert(new Country
            {
                Id = input.Id,
                Name = input.Name,
                RegionId = input.RegionId 
            });
            _countryView.Message(result);
        }

        public void Update()
        {
            var country = new Country();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    country = _countryView.UpdateCountry();
                    if (string.IsNullOrEmpty(country.Id) || string.IsNullOrEmpty(country.Name))
                    {
                        Console.WriteLine("Country ID/Name cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _country.Update(country);
            _countryView.Message(result);
        }

        public void Delete()
        {
            var country = new Country();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    country = _countryView.DeleteInput();
                    var data = _country.GetById(country.Id);
                    if (string.IsNullOrEmpty(data.Name))
                    {
                        Console.WriteLine($"No Country with Id = {data.Id}");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var result = _country.Delete(country.Id);
            _countryView.Message(result);
        }
    }
}
