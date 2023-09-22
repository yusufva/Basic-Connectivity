using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Models
{
    public class Country : Interface<Country, string>
    {
        private static Connect connectionString = new Connect(); //mengambil connection String
        private static string tableName = "countries"; //deklarasi nama tabel
        public string Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Name} - {RegionId}";
        }

        public Country()
        {
            Id = string.Empty;
            Name = string.Empty;
            RegionId = 0;
        }

        public Country(string Id, string Name, int RegionId)
        {
            this.Id = Id;
            this.Name = Name;
            this.RegionId = RegionId;
        }

        //Delete : Countries
        public string Delete(string id)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"DELETE FROM {tableName} WHERE id = @id"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
                    pId.ParameterName = "id"; //meng assign nama parameter
                    pId.Value = id; //meng assign value parameter
                    pId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada DB
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

        //Get All : Countries
        public List<Country> GetAll()
        {
            var countries = new List<Country>();

            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"SELECT * FROM {tableName}"; //command sql query yang akan dijalankan

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
                        countries.Add(new Country
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            RegionId = reader.GetInt32(2),
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return countries;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<Country>();
        }

        //Get By Id : Countries
        public Country GetById(string id)
        {
            var countryId = new Country();

            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"SELECT * FROM {tableName} WHERE id = @id"; //command sql query yang akan dijalankan
            SqlParameter pId = new SqlParameter(); //pendefinisian parameter baru
            pId.ParameterName = "id"; //meng assign nama parameter
            pId.Value = id; //meng assign value parameter
            pId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada DB
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
                        countryId.Id = reader.GetString(0);
                        countryId.Name = reader.GetString(1);
                        countryId.RegionId = reader.GetInt32(2);
                    }
                    reader.Close();
                    connection.Close();
                    return countryId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new Country();
        }

        //insert : countries
        public string Insert(Country country)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"INSERT INTO {tableName} VALUES (@id, @name, @regionId)"; //command sql query yang akan dijalankan


            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pId = new SqlParameter(); //meng assign parameter baru
                    pId.ParameterName = "Id"; // meng assign nama parameter pada sql
                    pId.Value = country.Id; //meng assign nilai parameter
                    pId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pId); //command menambahkan parameter pada query

                    SqlParameter pName = new SqlParameter(); //meng assign parameter baru
                    pName.ParameterName = "Name"; // meng assign nama parameter pada sql
                    pName.Value = country.Name; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    SqlParameter pRegionId = new SqlParameter(); //meng assign parameter baru
                    pRegionId.ParameterName = "regionId"; // meng assign nama parameter pada sql
                    pRegionId.Value = country.RegionId; //meng assign nilai parameter
                    pRegionId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pRegionId); //command menambahkan parameter pada query

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

        //Update : countries
        public string Update(Country country)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"UPDATE {tableName} SET  name = @name, region_id = @regionId WHERE id = @id"; //command sql query yang akan dijalankan


            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pId = new SqlParameter(); //meng assign parameter baru
                    pId.ParameterName = "Id"; // meng assign nama parameter pada sql
                    pId.Value = country.Id; //meng assign nilai parameter
                    pId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pId); //command menambahkan parameter pada query

                    SqlParameter pName = new SqlParameter(); //meng assign parameter baru
                    pName.ParameterName = "Name"; // meng assign nama parameter pada sql
                    pName.Value = country.Name; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    SqlParameter pRegionId = new SqlParameter(); //meng assign parameter baru
                    pRegionId.ParameterName = "regionId"; // meng assign nama parameter pada sql
                    pRegionId.Value = country.RegionId; //meng assign nilai parameter
                    pRegionId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pRegionId); //command menambahkan parameter pada query

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
    }
}
