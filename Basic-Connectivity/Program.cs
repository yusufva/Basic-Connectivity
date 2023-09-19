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
            //GetAllRegion();
            //InsertRegion("Jawa Tengah");
            //GetRegionById(11);
            //UpdateRegion(11, "Jawa Barat");
            DeleteRegion(12);
        }

        // GET ALL : Regions
        public static void GetAllRegion()
        {
            using var connection = new SqlConnection(connectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = "SELECT * FROM region"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi

                SqlDataReader reader = command.ExecuteReader(); //mengeksekusi kode dan menyimpan pada variabel reader

                if (reader.HasRows){ //pengkondisian jika reader memiliki baris
                    //output the column headers
                    while (reader.Read()) { // membaca baris pada reader
                        // menampilkan data yang didapat ke console
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                    }
                }
                else{ //pengkondisian jika tidak ditemukan row
                    Console.WriteLine("No Rows found.");
                }

                reader.Close(); //menutup reader
            } catch (Exception e){
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            } finally{
                connection.Close(); //menutup koneksi db
            }
        }

        // GET BY ID : Region
        public static void GetRegionById(int id)
        {
            using var connection = new SqlConnection(connectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = "SELECT * FROM region WHERE id = @id"; //command sql query yang akan dijalankan
            SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
            pId.ParameterName = "id"; //meng assign nama parameter
            pId.Value = id; //meng assign value parameter
            pId.SqlDbType = SqlDbType.Int; //meng assign tipe data parameter pada DB
            command.Parameters.Add(pId); //command menambahkan parameter ke dalam sql query

            try { 
                connection.Open(); //membuka koneksi

                SqlDataReader reader = command.ExecuteReader(); //mengeksekusi kode dan menyimpan pada variabel reader

                if (reader.HasRows) //pengkondisian jika reader memiliki baris
                {
                    while (reader.Read()) // membaca baris pada reader
                    {
                        // menampilkan data yang didapat ke console
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Name: "+ reader.GetString(1));
                    }
                }
                else //pengkondisian jika tidak ditemukan row
                {
                    Console.WriteLine($"Region with id: {id} not found");
                }

                reader.Close(); //menutup reader
            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            } finally
            {
                connection.Close(); //menutup koneksi db
            }
        }

        // INSERT: Region
        public static void InsertRegion(string name)
        {
            using var connection = new SqlConnection(connectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = "INSERT INTO region VALUES (@name)"; //command sql query yang akan dijalankan

            
            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pName = new SqlParameter(); //meng assign parameter baru
                    pName.ParameterName = "Name"; // meng assign nama parameter pada sql
                    pName.Value = name; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    command.Transaction = transaction; //command memulai transaksi

                    int result = command.ExecuteNonQuery(); //command melakukan eksekusi

                    transaction.Commit(); //melakukan commit transaksi jika eksekusi berhasil
                    connection.Close(); //menutup koneksi db

                    switch (result) //pengkondisian notifikasi
                    {
                        case >=1: //case jika result lebih dari atau sama dengan 1
                            Console.WriteLine("insert success");//menampilkan notifikasi berhasil
                            break;
                        default:
                            Console.WriteLine("insert failed");//menampilkan notifikasi gagal
                            break;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error transaction: {e.Message}");
                }

            } catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
        
        // Update : Region
        public static void UpdateRegion(int id, string name)
        {
            using var connection = new SqlConnection(connectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = "UPDATE region SET name = @name WHERE id = @id"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pName = new SqlParameter(); //meng assign parameter baru
                    pName.ParameterName = "Name"; // meng assign nama parameter pada sql
                    pName.Value = name; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
                    pId.ParameterName = "id"; //meng assign nama parameter
                    pId.Value = id; //meng assign value parameter
                    pId.SqlDbType = SqlDbType.Int; //meng assign tipe data parameter pada DB
                    command.Parameters.Add(pId); //command menambahkan parameter ke dalam sql query

                    command.Transaction = transaction; //command memulai transaksi

                    int result = command.ExecuteNonQuery(); //command melakukan eksekusi

                    transaction.Commit(); //melakukan commit transaksi jika eksekusi berhasil
                    connection.Close(); //menutup koneksi db

                    switch (result) //pengkondisian notifikasi
                    {
                        case >= 1: //case jika result lebih dari atau sama dengan 1
                            Console.WriteLine("Update success");//menampilkan notifikasi berhasil
                            break;
                        default:
                            Console.WriteLine("Upadate failed");//menampilkan notifikasi gagal
                            break;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback(); //melakukan rollback ketika error
                    Console.WriteLine($"Error transaction: {e.Message}"); //menampilkan pesan error
                }

            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan error
            }
        }

        // Delete : Region
        public static void DeleteRegion(int id)
        {
            using var connection = new SqlConnection(connectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = "DELETE FROM region WHERE id = @id"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
                    pId.ParameterName = "id"; //meng assign nama parameter
                    pId.Value = id; //meng assign value parameter
                    pId.SqlDbType = SqlDbType.Int; //meng assign tipe data parameter pada DB
                    command.Parameters.Add(pId); //command menambahkan parameter ke dalam sql query

                    command.Transaction = transaction; //command memulai transaksi

                    int result = command.ExecuteNonQuery(); //command melakukan eksekusi

                    transaction.Commit(); //melakukan commit transaksi jika eksekusi berhasil
                    connection.Close(); //menutup koneksi db

                    switch (result) //pengkondisian notifikasi
                    {
                        case >= 1: //case jika result lebih dari atau sama dengan 1
                            Console.WriteLine("Delete success");//menampilkan notifikasi berhasil
                            break;
                        default:
                            Console.WriteLine("Delete failed");//menampilkan notifikasi gagal
                            break;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback(); //melakukan rollback ketika error
                    Console.WriteLine($"Error transaction: {e.Message}"); //menampilkan pesan error
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan error
            }
        }
    }
}