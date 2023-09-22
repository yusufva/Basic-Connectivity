using Basic_Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Views
{
    public class RegionView : GeneralView
    {
        public string InsertInput()
        {
            Console.WriteLine("Insert region name");
            var name = Console.ReadLine();

            return name;
        }

        public Region DeleteInput()
        {
            Console.WriteLine("Insert region id");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            return new Region { Id=id,Name=""};
        }

        public Region UpdateRegion()
        {
            Console.WriteLine("Insert region id");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Insert region name");
            var name = Console.ReadLine();

            return new Region
            {
                Id = id,
                Name = name
            };
        }
    }
}
