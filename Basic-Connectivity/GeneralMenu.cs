using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public class GeneralMenu
    {
        public static void List<T>(List<T> items, string title)
        {
            Console.WriteLine("---------------");
            Console.WriteLine($"List of {title}");
            Console.WriteLine("---------------");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("---------------");
        }
    }
}
