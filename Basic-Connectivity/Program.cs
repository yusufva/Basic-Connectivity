using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Basic_Connectivity
{
    internal class Program
    {
        // connection string untuk menghubungkan dengan database
        static string connectionString = "Data Source=DESKTOP-A42IQOB;Database=db_hr_dts;Connect Timeout=30;Integrated Security=True";

        //program utama
        static void Main(string[] args)
        {
            //pemanggilan method - method yang telah dibuat
            var region = new Region();
            var getAllRegion = region.GetAll();
            //InsertRegion("Jawa Tengah");
            //GetRegionById(11);
            //UpdateRegion(11, "Jawa Barat");
            //DeleteRegion(12);
        }

        
    }
}