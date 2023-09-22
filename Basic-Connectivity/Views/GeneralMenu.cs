using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Views
{
    public class GeneralView
    {
        //General method untuk menampilkan hasil Get
        public void List<T>(List<T> items, string title)
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

        public void Message(string message)
        {
            Console.WriteLine("---------------");
            int value;
            int.TryParse(message, out value);
            if (value>0)
            {
                Console.WriteLine($"Success, {value} Row Affected");
            }
            else
            {
                Console.WriteLine("Failed to modify data");
            }
            Console.WriteLine("---------------");
        }

        public void Single<T>(T item, string title)
        {
            Console.WriteLine($"List of {title}");
            Console.WriteLine("---------------");
            Console.WriteLine(item.ToString());
        }
    }
}
