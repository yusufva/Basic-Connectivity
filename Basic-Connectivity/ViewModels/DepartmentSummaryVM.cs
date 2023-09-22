using Basic_Connectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.ViewModels
{
    public class DepartmentSummaryVM 
    {
        //pengefinisian property
        public string departmentName { get; set; }
        public int? totalEmployee { get; set; }
        public int? minSalary { get; set; }
        public int? maxSalary { get; set; }
        public double? averageSalary { get; set; }

        //melakukan override general method
        public override string ToString()
        {
            return $"{departmentName} - {totalEmployee} - {minSalary} - {maxSalary} - {averageSalary}";
        }
    }
}
