namespace Basic_Connectivity
{
    internal class Program
    {

        //program utama
        static void Main(string[] args)
        {
            var choice = true;
            //perulangan agar program selalu berjalan
            while (choice)
            {
                //pemilihan menu utama
                Console.WriteLine("1. Get all");
                Console.WriteLine("2. Insert");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. exit");
                
                Console.Write("Enter your choice: ");
                var input = Console.ReadLine(); //input pemilihan menu
                if (input == "1") // menu 1
                {
                    Console.WriteLine();
                    Console.WriteLine("1. List all regions");
                    Console.WriteLine("2. List all countries");
                    Console.WriteLine("3. List all locations");
                    Console.WriteLine("4. List all departments");
                    Console.WriteLine("5. List all jobs");
                    Console.WriteLine("6. List all histories");
                    Console.WriteLine("7. List all employees");
                    Console.WriteLine("8. Exit");
                    Console.Write("Enter your choice: ");
                    var menu = Console.ReadLine();
                    choice = Get(menu);

                }
                else if (input == "2") // menu 2
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Add regions");
                    Console.WriteLine("2. Add countries");
                    Console.WriteLine("3. Add locations");
                    Console.WriteLine("4. Add departments");
                    Console.WriteLine("5. Add jobs");
                    Console.WriteLine("6. Add histories");
                    Console.WriteLine("7. Add employees");
                    Console.WriteLine("8. Exit");
                    Console.Write("Enter your choice: ");
                    var menu = Console.ReadLine();
                    choice = Insert(menu);

                } else if (input=="3") // menu 3
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Update regions");
                    Console.WriteLine("2. Update countries");
                    Console.WriteLine("3. Update locations");
                    Console.WriteLine("4. Update departments");
                    Console.WriteLine("5. Update jobs");
                    Console.WriteLine("6. Update histories");
                    Console.WriteLine("7. Update employees");
                    Console.WriteLine("8. Exit");
                    Console.Write("Enter your choice: ");
                    var menu = Console.ReadLine();
                    choice = Insert(menu);

                } else if (input == "4") // menu 4
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Delete regions");
                    Console.WriteLine("2. Delete countries");
                    Console.WriteLine("3. Delete locations");
                    Console.WriteLine("4. Delete departments");
                    Console.WriteLine("5. Delete jobs");
                    Console.WriteLine("6. Delete histories");
                    Console.WriteLine("7. Delete employees");
                    Console.WriteLine("8. Exit");
                    Console.Write("Enter your choice: ");
                    var menu = Console.ReadLine();
                    choice = Insert(menu);

                }
                else if (input == "5") //menu 5
                {
                    choice = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice");
                    Console.WriteLine();
                }
            }
        }
        public static bool Get(string input) //method pemilihan menu get
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

        public static bool Insert(string input) //method pemilihan menu insert
        {
            switch (input)
            {
                case "1":
                    Console.Write("Input Region Name: ");
                    var regionName = Console.ReadLine();
                    var region = new Region();
                    region.Name = regionName;
                    var regions = region.Insert(region);
                    GeneralMenu.Message(regions);
                    break;
                case "2":
                    Console.Write("Input Country Id (2 character): ");
                    var countryId = Console.ReadLine();
                    Console.Write("Input Country Name: ");
                    var countryName = Console.ReadLine();
                    Console.Write("Input Country Region Id(Number): ");
                    int countryRegionId;
                    int.TryParse(Console.ReadLine(), out countryRegionId);    
                    var country = new Country(countryId, countryName,countryRegionId);
                    var countries = country.Insert(country);
                    GeneralMenu.Message(countries);
                    break;
                case "3":
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
                    var location = new Locations(locationsId, locationStreet, locationPostal, locationCity, locationProvince, locationCountryId);
                    var locations = location.Insert(location);
                    GeneralMenu.Message(locations);
                    break;
                case "4":
                    Console.Write("Input Department Id (number): ");
                    int departmentsId;
                    int.TryParse(Console.ReadLine(), out departmentsId);
                    Console.Write("Input Department Name: ");
                    var departmentName = Console.ReadLine();
                    Console.Write("Input Department Location Id: ");
                    int departmentLocation;
                    int.TryParse(Console.ReadLine(), out departmentLocation);
                    Console.Write("Input Department Manager Id: ");
                    int departmentManager;
                    int.TryParse(Console.ReadLine(), out departmentManager);
                    var department = new Departments(departmentsId, departmentName, departmentLocation, departmentManager);
                    var departments = department.Insert(department);
                    GeneralMenu.Message(departments);
                    break;
                case "5":
                    Console.Write("Input Jobs Id (2 character): ");
                    var jobsId = Console.ReadLine();
                    Console.Write("Input Jobs Title: ");
                    var jobsTitle = Console.ReadLine();
                    Console.Write("Input Jobs Min Salary: ");
                    int jobsMinSalary;
                    int.TryParse(Console.ReadLine(), out jobsMinSalary);
                    Console.Write("Input Jobs Max Salary: ");
                    int jobsMaxSalary;
                    int.TryParse(Console.ReadLine(), out jobsMaxSalary);
                    var job = new Jobs(jobsId, jobsTitle, jobsMinSalary, jobsMaxSalary);
                    var jobs = job.Insert(job);
                    GeneralMenu.Message(jobs);
                    break;
                case "6":
                    Console.Write("Input Employee Id (Number): ");
                    int historyEmployeeId;
                    int.TryParse(Console.ReadLine(), out historyEmployeeId);
                    Console.Write("Input Department Id (Number): ");
                    int historyDepartment;
                    int.TryParse(Console.ReadLine(), out historyDepartment);
                    Console.Write("Input Job Id (2 character): ");
                    var historyJob = Console.ReadLine();
                    var startDate = DateTime.Now;
                    var date = new DateTime();
                    var history = new History(startDate, date, historyEmployeeId, historyDepartment, historyJob);
                    var histories = history.Insert(history);
                    GeneralMenu.Message(histories);
                    break;
                case "7":
                    Console.Write("Input Employee Id (Number): ");
                    int employeeId;
                    int.TryParse(Console.ReadLine(), out employeeId);
                    Console.Write("Input Employee First Name: ");
                    var employeeFirst = Console.ReadLine();
                    Console.Write("Input Employee Last Name: ");
                    var employeeLast = Console.ReadLine();
                    Console.Write("Input Employee Email: ");
                    var employeeEmail = Console.ReadLine();
                    Console.Write("Input Employee Phone Number: ");
                    var employeePhone = Console.ReadLine();
                    Console.Write("Input Employee Salary: ");
                    int employeeSalary;
                    int.TryParse(Console.ReadLine(),out employeeSalary);
                    Console.Write("Input Employee Comission PCT: ");
                    decimal employeePct;
                    decimal.TryParse(Console.ReadLine(), out employeePct);
                    Console.Write("Input Employee Manager Id(Number): ");
                    int employeeManager;
                    int.TryParse(Console.ReadLine(), out employeeManager);
                    Console.Write("Input Employee Job Id (2 character): ");
                    var employeeJob = Console.ReadLine();
                    Console.Write("Input Employee Department Id (Number): ");
                    int employeeDepartment;
                    int.TryParse(Console.ReadLine(), out employeeDepartment);
                    var employee = new Employee(employeeId,employeeFirst,employeeLast,employeeEmail,employeePhone,DateTime.Now,employeeSalary, employeePct, employeeManager, employeeJob, employeeDepartment);
                    var employees = employee.Insert(employee);
                    GeneralMenu.Message(employees);
                    break;
                case "8":
                    return false;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return true;
        }
        public static bool Update(string input) //method pemilihan menu Update
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
        public static bool Delete(string input) //method pemilihan menu Delete
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