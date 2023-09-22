using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Models
{
    public class Connect
    {
        public Connect()
        {
            ConnectionString = "Data Source=DESKTOP-A42IQOB;Database=db_hr_dts;Connect Timeout=30;Integrated Security=True";
        }

        public string ConnectionString { get; set; }
    }
}
