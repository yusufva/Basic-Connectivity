using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public class Region : Interface<Region,int>
    {
        private static Connect connectionString = new Connect();
        public int Id { get; set; }
        public string Name { get; set; }

        public Region() { 
            Id = 0;
            Name = null;
        }

        public Region(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }

        // GET ALL : Regions
        public List<Region> GetAll()
        {
            var regions = new List<Region>();

            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = "SELECT * FROM region"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi

                SqlDataReader reader = command.ExecuteReader(); //mengeksekusi kode dan menyimpan pada variabel reader

                if (reader.HasRows)
                { //pengkondisian jika reader memiliki baris
                    //output the column headers
                    while (reader.Read())
                    { // membaca baris pada reader
                        // menampilkan data yang didapat ke console
                        regions.Add(new Region
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        }) ;
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return regions;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<Region>();
        }

        // GET BY ID : Region
        public Region GetById(int id)
        {
            var regionId = new Region();

            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = "SELECT * FROM region WHERE id = @id"; //command sql query yang akan dijalankan
            SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
            pId.ParameterName = "id"; //meng assign nama parameter
            pId.Value = id; //meng assign value parameter
            pId.SqlDbType = SqlDbType.Int; //meng assign tipe data parameter pada DB
            command.Parameters.Add(pId); //command menambahkan parameter ke dalam sql query

            try
            {
                connection.Open(); //membuka koneksi

                SqlDataReader reader = command.ExecuteReader(); //mengeksekusi kode dan menyimpan pada variabel reader

                if (reader.HasRows) //pengkondisian jika reader memiliki baris
                {
                    while (reader.Read()) // membaca baris pada reader
                    {
                        // menampilkan data yang didapat ke console
                        regionId.Id = reader.GetInt32(0);
                        regionId.Name = reader.GetString(1) ;
                    }
                    reader.Close();
                    connection.Close();
                    return regionId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new Region();
        }

        // INSERT: Region
        public string Insert(Region region)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
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
                    pName.Value = region.Name; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    command.Transaction = transaction; //command memulai transaksi

                    int result = command.ExecuteNonQuery(); //command melakukan eksekusi

                    transaction.Commit(); //melakukan commit transaksi jika eksekusi berhasil
                    connection.Close(); //menutup koneksi db

                    return result.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return $"Error transaction: {e.Message}";
                }

            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }

        // Update : Region
        public string Update(Region region)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
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
                    pName.Value = region.Name; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
                    pId.ParameterName = "id"; //meng assign nama parameter
                    pId.Value = region.Id; //meng assign value parameter
                    pId.SqlDbType = SqlDbType.Int; //meng assign tipe data parameter pada DB
                    command.Parameters.Add(pId); //command menambahkan parameter ke dalam sql query

                    command.Transaction = transaction; //command memulai transaksi

                    int result = command.ExecuteNonQuery(); //command melakukan eksekusi

                    transaction.Commit(); //melakukan commit transaksi jika eksekusi berhasil
                    connection.Close(); //menutup koneksi db

                    return result.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback(); //melakukan rollback ketika error
                    return $"Error transaction: {e.Message}"; //menampilkan pesan error
                }

            }
            catch (Exception e)
            {
                return $"Error: {e.Message}"; //menampilkan pesan error
            }
        }

        // Delete : Region
        public string Delete(int id)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
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

                    return result.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback(); //melakukan rollback ketika error
                    return $"Error transaction: {e.Message}"; //menampilkan pesan error
                }

            }
            catch (Exception e)
            {
                return $"Error: {e.Message}"; //menampilkan pesan error
            }
        }
    }
}
