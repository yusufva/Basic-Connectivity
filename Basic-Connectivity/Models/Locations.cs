using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Basic_Connectivity.Models
{
    public class Locations : Interface<Locations, int>
    {
        Connect connectionString = new Connect(); //mengambil connection String
        private static string tableName = "locations"; //deklarasi nama tabel
        public int Id { get; set; }
        public string streetAddress { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string statProvince { get; set; }
        public string countryId { get; set; }

        public override string ToString()
        {
            return $"{Id} - {streetAddress} - {postalCode} - {city} - {statProvince} - {countryId}";
        }

        public Locations()
        {
            Id = 0;
            streetAddress = string.Empty;
            postalCode = string.Empty;
            city = string.Empty;
            statProvince = string.Empty;
            countryId = string.Empty;
        }

        public Locations(int id, string streetAddress, string postalCode, string city, string statProvince, string country)
        {
            Id = id;
            this.streetAddress = streetAddress;
            this.postalCode = postalCode;
            this.city = city;
            this.statProvince = statProvince;
            countryId = country;
        }
        public string Delete(int id)
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

        public List<Locations> GetAll()
        {
            var locations = new List<Locations>();

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
                        locations.Add(new Locations
                        {
                            Id = reader.GetInt32(0),
                            streetAddress = reader.GetString(1),
                            postalCode = reader.GetString(2),
                            city = reader.GetString(3),
                            statProvince = reader.GetString(4),
                            countryId = reader.GetString(5)
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return locations;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<Locations>();
        }

        public Locations GetById(int id)
        {
            var location = new Locations();

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
                        location.Id = reader.GetInt32(0);
                        location.streetAddress = reader.GetString(1);
                        location.postalCode = reader.GetString(2);
                        location.city = reader.GetString(3);
                        location.statProvince = reader.GetString(4);
                        location.countryId = reader.GetString(5);
                    }
                    reader.Close();
                    connection.Close();
                    return location;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new Locations();
        }

        public string Insert(Locations locations)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"INSERT INTO {tableName} VALUES (@id, @streetAddres, @postalCode, @city, @statProvince, @countryId)"; //command sql query yang akan dijalankan


            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pId = new SqlParameter(); //meng assign parameter baru
                    pId.ParameterName = "Id"; // meng assign nama parameter pada sql
                    pId.Value = locations.Id; //meng assign nilai parameter
                    pId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pId); //command menambahkan parameter pada query

                    SqlParameter pName = new SqlParameter(); //meng assign parameter baru
                    pName.ParameterName = "streetAddress"; // meng assign nama parameter pada sql
                    pName.Value = locations.streetAddress; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    SqlParameter pRegionId = new SqlParameter(); //meng assign parameter baru
                    pRegionId.ParameterName = "countryId"; // meng assign nama parameter pada sql
                    pRegionId.Value = locations.countryId; //meng assign nilai parameter
                    pRegionId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pRegionId); //command menambahkan parameter pada query

                    command.Parameters.Add(new SqlParameter("@postalCode", locations.postalCode)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@city", locations.city)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@statProvince", locations.statProvince)); //command menambahkan parameter pada query

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

        public string Update(Locations locations)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"UPDATE {tableName} SET street_address = @streetAddress, postal_code = @postalCode, city = @city, stat_province = @statProvince, country_id = @countryId WHERE id = @id)"; //command sql query yang akan dijalankan


            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    SqlParameter pId = new SqlParameter(); //meng assign parameter baru
                    pId.ParameterName = "Id"; // meng assign nama parameter pada sql
                    pId.Value = locations.Id; //meng assign nilai parameter
                    pId.SqlDbType = SqlDbType.Int; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pId); //command menambahkan parameter pada query

                    SqlParameter pName = new SqlParameter(); //meng assign parameter baru
                    pName.ParameterName = "streetAddress"; // meng assign nama parameter pada sql
                    pName.Value = locations.streetAddress; //meng assign nilai parameter
                    pName.SqlDbType = SqlDbType.VarChar; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pName); //command menambahkan parameter pada query

                    SqlParameter pRegionId = new SqlParameter(); //meng assign parameter baru
                    pRegionId.ParameterName = "countryId"; // meng assign nama parameter pada sql
                    pRegionId.Value = locations.countryId; //meng assign nilai parameter
                    pRegionId.SqlDbType = SqlDbType.Char; //meng assign tipe data parameter pada db
                    command.Parameters.Add(pRegionId); //command menambahkan parameter pada query

                    command.Parameters.Add(new SqlParameter("@postalCode", locations.postalCode)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@city", locations.city)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@statProvince", locations.statProvince)); //command menambahkan parameter pada query


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
