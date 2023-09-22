using Basic_Connectivity.Models;
using Basic_Connectivity.ViewModels;
using Basic_Connectivity.Controllers;
using Basic_Connectivity.Views;

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
                Console.WriteLine("5. Employee Detail");
                Console.WriteLine("6. Department Summary");
                Console.WriteLine("7. exit");
                
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
                    //instansiasi objek yang dibutuhkan
                    var employee = new Employee();
                    var department = new Departments();
                    var locations = new Locations();
                    var country = new Country();
                    var Region = new Region();

                    //pendeklarasian variabel untuk mengambil smua data
                    var getEmployee = employee.GetAll();
                    var getDepartment = department.GetAll();
                    var getLocations = locations.GetAll();
                    var getCountry = country.GetAll();
                    var GetRegion = Region.GetAll();

                    var resultJoin = getEmployee //memulai LinQ dengan getEmployee
                        .Join(getDepartment, ed => ed.departmentId, d => d.Id, (ed, d) => new { ed, d }) //melakukan join LinQ getEmployee dengan getDepartment
                        .Join(getLocations, el => el.d.locationId, l => l.Id, (el, l) => new { el.ed, el.d, l }) //melakukan join LinQ tambahan dengan tabel locations
                        .Join(getCountry, ec => ec.l.countryId, c => c.Id, (ec, c) => new { ec.ed, ec.d, ec.l, c }) //melakukan join LinQ selanjutnya dengan tabel country
                        .Join(GetRegion, er => er.l.Id, r => r.Id, (er, r) => new EmployeeJoinsVM //melanjutkan join dengan tabel region kemudian mendefinisikan objek output
                        {
                            employeeId = er.ed.id, //mendefinisikan employee id yang diambil dari hasil join
                            fullName = er.ed.firstName + " " + er.ed.lastName, //mendefinisikan fullname yang diambil dari hasil join
                            email = er.ed.email, //mendefinisikan email yang diambil dari hasil join
                            phoneNumber = er.ed.phoneNumber, //mendefinisikan phone number yang diambil dari hasil join
                            salary = er.ed.salary, //mendefinisikan salary yang diambil dari hasil join
                            departmentName = er.d.Name, //mendefinisikan department name yang diambil dari hasil join
                            streetAddress = er.l.streetAddress, //mendefinisikan location street address yang diambil dari hasil join
                            countryName = er.c.Name, //mendefinisikan country name yang diambil dari hasil join
                            regionName = r.Name //mendefinisikan region name yang diambil dari hasil join
                        }).ToList(); //mengubah object yang dihasilkan menjadi list
                    //RegionView.List(resultJoin, "Employee Detail"); //menampilkan output 
                }
                else if (input == "6") //menu 6
                {
                    //instansiasi objek yang dibutuhkan
                    var employee1 = new Employee();
                    var department1 = new Departments();

                    //pendeklarasian variabel untuk mengambil smua data
                    var getEmployee1 = employee1.GetAll();
                    var getDepartment1 = department1.GetAll();

                    var resultJoin1 = getEmployee1 //memulai LinQ dengan getEmployee
                        .Join(getDepartment1, e => e.departmentId, d => d.Id, (e, d) => new { e, d }) //melakukan join LinQ getEmployee dengan getDepartment
                        .GroupBy(ed => ed.d.Name) //melakukan group by hasil join dengan department name
                        .Where(grouped => grouped.Count() > 3) //memberikan kondisi dimana hasil yang ditampilkan ketika karyawan > 3
                        .Select(grouped => new DepartmentSummaryVM //melakukan select untuk menampilkan data yang diambil
                        {
                            departmentName = grouped.Key, //mendefinisikan department name yang diambil dari hasil LinQ
                            totalEmployee = grouped.Count(), //mendefinisikan total employee yang diambil dari hasil LinQ
                            minSalary = grouped.Min(ed => ed.e.salary), //mendefinisikan min salary yang diambil dari hasil LinQ
                            maxSalary = grouped.Max(ed => ed.e.salary), //mendefinisikan max salary yang diambil dari hasil LinQ
                            averageSalary = grouped.Average(ed => ed.e.salary) //mendefinisikan average salary yang diambil dari hasil LinQ
                        }).ToList(); //mengubah object yang dihasilkan menjadi list
                    //DepartmentSummaryVM.List(resultJoin1, "Department Summary"); //menampilkan output
                }
                else if (input == "7") //menu 5
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
                    var regionView = new RegionView();
                    var regionController = new RegionController(region, regionView);
                    regionController.GetAll();
                    /*GeneralView.List(regions, "regions");*/
                    break;
                case "2":
                    var country = new Country();
                    var countries = country.GetAll();
                    /*GeneralView.List(countries, "countries");*/
                    break;
                case "3":
                    var location = new Locations();
                    var locations = location.GetAll();
                    /*GeneralView.List(locations, "locations");*/
                    break;
                case "4":
                    var department = new Departments();
                    var departments = department.GetAll();
                    /*GeneralView.List(departments, "departments");*/
                    break;
                case "5":
                    var job = new Jobs();
                    var jobs = job.GetAll();
                    /*GeneralView.List(jobs, "jobs");*/
                    break;
                case "6":
                    var history = new History();
                    var histories = history.GetAll();
                    //GeneralView.List(histories, "histories");
                    break;
                case "7":
                    var employee = new Employee();
                    var employees = employee.GetAll();
                    /*GeneralView.List(employees, "employees");*/
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
                    var region = new Region();
                    var regionView = new RegionView();
                    var regionController = new RegionController(region, regionView);
                    regionController.Insert();
                    /*GeneralView.Message(regions);*/
                    break;
                case "2":
                    var country = new Region();
                    var countryView = new RegionView();
                    var countryController = new RegionController(country, countryView);
                    countryController.Insert();
                    /*GeneralView.Message(countries);*/
                    break;
                case "3":
                    
                    var location = new Locations(locationsId, locationStreet, locationPostal, locationCity, locationProvince, locationCountryId);
                    var locations = location.Insert(location);
                    /*GeneralView.Message(locations);*/
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
                    /*GeneralView.Message(departments);*/
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
                    /*GeneralView.Message(jobs);*/
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
                    /*GeneralView.Message(histories);*/
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
                    /*GeneralView.Message(employees);*/
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
                    /*GeneralView.List(regions, "regions");*/
                    break;
                case "2":
                    var country = new Country();
                    var countries = country.GetAll();
                    /*GeneralView.List(countries, "countries");*/
                    break;
                case "3":
                    var location = new Locations();
                    var locations = location.GetAll();
                    /*GeneralView.List(locations, "locations");*/
                    break;
                case "4":
                    var department = new Departments();
                    var departments = department.GetAll();
                    /*GeneralView.List(departments, "departments");*/
                    break;
                case "5":
                    var job = new Jobs();
                    var jobs = job.GetAll();
                    /*GeneralView.List(jobs, "jobs");*/
                    break;
                case "6":
                    var history = new History();
                    var histories = history.GetAll();
                    /*GeneralView.List(histories, "histories");*/
                    break;
                case "7":
                    var employee = new Employee();
                    var employees = employee.GetAll();
                    /*GeneralView.List(employees, "employees");*/
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
                    /*GeneralView.List(regions, "regions");*/
                    break;
                case "2":
                    var country = new Country();
                    var countries = country.GetAll();
                    /*GeneralView.List(countries, "countries");*/
                    break;
                case "3":
                    var location = new Locations();
                    var locations = location.GetAll();
                    /*GeneralView.List(locations, "locations");*/
                    break;
                case "4":
                    var department = new Departments();
                    var departments = department.GetAll();
                    /*GeneralView.List(departments, "departments");*/
                    break;
                case "5":
                    var job = new Jobs();
                    var jobs = job.GetAll();
                    /*GeneralView.List(jobs, "jobs");*/
                    break;
                case "6":
                    var history = new History();
                    var histories = history.GetAll();
                    /*GeneralView.List(histories, "histories");*/
                    break;
                case "7":
                    var employee = new Employee();
                    var employees = employee.GetAll();
                    /*GeneralView.List(employees, "employees");*/
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