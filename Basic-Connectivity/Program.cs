using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Basic_Connectivity
{
    internal class Program
    {

        //program utama
        static void Main(string[] args)
        {
            var choice = true;
            while (choice)
            {
                Console.WriteLine("1. List all regions");
                Console.WriteLine("2. List all countries");
                Console.WriteLine("3. List all locations");
                Console.WriteLine("4. List all departments");
                Console.WriteLine("5. List all jobs");
                Console.WriteLine("6. List all histories");
                Console.WriteLine("7. List all employees");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                var input = Console.ReadLine();
                if (input == "9")
                {
                    Console.WriteLine("1. List all regions");
                    Console.WriteLine("2. List all countries");
                    Console.WriteLine("3. List all locations");
                    Console.WriteLine("4. List all departments");
                    Console.WriteLine("5. List all jobs");
                    Console.WriteLine("6. List all histories");
                    Console.WriteLine("7. List all employees");
                    Console.WriteLine("8. Exit");
                    Console.Write("Enter your choice: ");
                    var add = Console.ReadLine();
                }
                choice = Menu(input);
            }
        }
        public static bool Menu(string input)
        {
            switch (input)
            {
                case "1":
                    var region = new Region();
                    var regions = region.GetAll();
                    GeneralMenu.List(regions, "regions");
                    break;
                case "2":
                    var country = new Country();
                    var countries = country.GetAll();
                    GeneralMenu.List(countries, "countries");
                    break;
                case "3":
                    var location = new Locations();
                    var locations = location.GetAll();
                    GeneralMenu.List(locations, "locations");
                    break;
                case "4":
                    var department = new Departments();
                    var departments = department.GetAll();
                    GeneralMenu.List(departments, "departments");
                    break;
                case "5":
                    var job = new Jobs();
                    var jobs = job.GetAll();
                    GeneralMenu.List(jobs, "jobs");
                    break;
                case "6":
                    var history = new History();
                    var histories = history.GetAll();
                    GeneralMenu.List(histories, "histories");
                    break;
                case "7":
                    var employee = new Employee();
                    var employees = employee.GetAll();
                    GeneralMenu.List(employees, "employees");
                    break;
                case "8":
                    return false;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return true;
        }

        public static bool Insert(string input)
        {
            switch (input)
            {
                case "1":
                    var region = new Region();
                    var regions = region.GetAll();
                    GeneralMenu.List(regions, "regions");
                    break;
                case "2":
                    var country = new Country();
                    var countries = country.GetAll();
                    GeneralMenu.List(countries, "countries");
                    break;
                case "3":
                    var location = new Locations();
                    var locations = location.GetAll();
                    GeneralMenu.List(locations, "locations");
                    break;
                case "4":
                    var department = new Departments();
                    var departments = department.GetAll();
                    GeneralMenu.List(departments, "departments");
                    break;
                case "5":
                    var job = new Jobs();
                    var jobs = job.GetAll();
                    GeneralMenu.List(jobs, "jobs");
                    break;
                case "6":
                    var history = new History();
                    var histories = history.GetAll();
                    GeneralMenu.List(histories, "histories");
                    break;
                case "7":
                    var employee = new Employee();
                    var employees = employee.GetAll();
                    GeneralMenu.List(employees, "employees");
                    break;
                case "8":
                    return false;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return true;
        }
    }
}