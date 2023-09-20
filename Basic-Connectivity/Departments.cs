using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public class Departments : Interface<Departments, int>
    {
        Connect connectionString = new Connect();  //mengambil connection String
        private static string tableName = "departments"; //deklarasi nama tabel
        public int Id { get; set; }
        public string Name { get; set; }
        public int locationId { get; set; }
        public int managerId { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name} - {locationId} - {managerId}";
        }

        public Departments() { 
            this.Id = 0;
            this.Name = string.Empty;
            this.managerId = 0;
            this.locationId = 0;
        }

        public Departments(int id, string name, int locationId, int managerId)
        {
            Id = id;
            Name = name;
            this.locationId = locationId;
            this.managerId = managerId;
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
                    command.Parameters.Add(new SqlParameter("@id", id)); //command menambahkan parameter ke dalam sql query

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

        public List<Departments> GetAll()
        {
            var departments = new List<Departments>();

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
                        // memasukkan data setiap department kedalam list
                        departments.Add(new Departments
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            locationId = reader.GetInt32(2),
                            managerId = reader.GetInt32(3)
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return departments;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<Departments>();
        }

        public Departments GetById(int id)
        {
            var department = new Departments();

            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"SELECT * FROM {tableName} WHERE id = @id"; //command sql query yang akan dijalankan
            command.Parameters.Add(new SqlParameter("@id", id)); //command menambahkan parameter ke dalam sql query

            try
            {
                connection.Open(); //membuka koneksi

                SqlDataReader reader = command.ExecuteReader(); //mengeksekusi kode dan menyimpan pada variabel reader

                if (reader.HasRows) //pengkondisian jika reader memiliki baris
                {
                    while (reader.Read()) // membaca baris pada reader
                    {
                        // menampilkan data yang didapat ke console
                        Id = reader.GetInt32(0);
                        Name = reader.GetString(1);
                        locationId = reader.GetInt32(2);
                        managerId = reader.GetInt32(3);
                    }
                    reader.Close();
                    connection.Close();
                    return department;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new Departments();
        }

        public string Insert(Departments departments)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"INSERT INTO {tableName} VALUES (@id, @name, @locationId, @managerId)"; //command sql query yang akan dijalankan


            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", departments.Id)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@name", departments.Name)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@locationId", departments.locationId)); //command menambahkan parameter pada query

                    command.Parameters.Add(new SqlParameter("@managerId", departments.managerId)); //command menambahkan parameter pada query

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

        public string Update(Departments departments)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"UPDATE {tableName} SET name = @name, location_id = @locationid, manager_id = @managerId WHERE id = @id)"; //command sql query yang akan dijalankan


            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", departments.Id)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@name", departments.Name)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@locationId", departments.locationId)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@managerId", departments.managerId)); //command menambahkan parameter pada query

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
