using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public class EmployeeJoinsVM
    {
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime hireDate { get; set; }
        public int? salary { get; set; }
        public decimal commisionPct { get; set; }
        public int? managerId { get; set; }
        public string departmentName { get; set; }
        public string locations { get; set; }
        public string countryName{ get; set; }
        public string regionName{ get; set; }
        public override string ToString()
        {
            return $"{employeeId} - {firstName} - {lastName} - {email} - {phoneNumber} - {hireDate} - {salary} - {commisionPct} - {managerId} - {departmentName} - {locations} - {countryName} - {regionName}";
        }
    }
}
