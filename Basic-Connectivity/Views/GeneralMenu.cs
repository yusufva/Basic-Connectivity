using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Views
{
    public class GeneralMenu
    {
        //General method untuk menampilkan hasil Get
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

        public static void Message(string message)
        {
            Console.WriteLine("---------------");
            int value;
            if (int.TryParse(message, out value))
            {
                Console.WriteLine($"Success, {value} Row Affected");
            }
            else
            {
                Console.WriteLine("Failed to modify data");
            }
            Console.WriteLine("---------------");
        }
    }
}
