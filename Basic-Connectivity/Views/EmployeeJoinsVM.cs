using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Views
{
    public class EmployeeJoinsVM
    {
        //pengefinisian property
        public int employeeId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime hireDate { get; set; }
        public int? salary { get; set; }
        public string streetAddress { get; set; }
        public int? managerId { get; set; }
        public string departmentName { get; set; }
        public string locations { get; set; }
        public string countryName { get; set; }
        public string regionName { get; set; }

        //melakukan override general method
        public override string ToString()
        {
            return $"{employeeId} - {fullName} - {email} - {phoneNumber} - {hireDate} - {salary} - {streetAddress} - {managerId} - {departmentName} - {locations} - {countryName} - {regionName}";
        }
    }
}
