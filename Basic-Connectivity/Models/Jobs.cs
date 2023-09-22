using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Basic_Connectivity.Models
{
    public class Jobs : Interface<Jobs, string>
    {
        Connect connectionString = new Connect(); //mengambil connection String
        private static string tableName = "jobs"; //deklarasi nama tabel
        public string id { get; set; }
        public string title { get; set; }
        public int minSalary { get; set; }
        public int maxSalary { get; set; }

        public override string ToString()
        {
            return $"{id} - {title} - {minSalary} - {maxSalary}";
        }

        public Jobs()
        {
            id = string.Empty;
            title = string.Empty;
            minSalary = 0;
            maxSalary = 0;
        }

        public Jobs(string id, string title, int minSalary, int maxSalary)
        {
            this.id = id;
            this.title = title;
            this.minSalary = minSalary;
            this.maxSalary = maxSalary;
        }

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

        public List<Jobs> GetAll()
        {
            var jobs = new List<Jobs>();

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
                        jobs.Add(new Jobs
                        {
                            id = reader.GetString(0),
                            title = reader.GetString(1),
                            minSalary = reader.GetInt32(2),
                            maxSalary = reader.GetInt32(3)
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return jobs;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<Jobs>();
        }

        public Jobs GetById(string id)
        {
            var jobs = new Jobs();

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
                        id = reader.GetString(0);
                        title = reader.GetString(1);
                        minSalary = reader.GetInt32(2);
                        maxSalary = reader.GetInt32(3);
                    }
                    reader.Close();
                    connection.Close();
                    return jobs;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new Jobs();
        }

        public string Insert(Jobs jobs)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"INSERT INTO {tableName} VALUES (@id, @title, @minSalary, @maxSalary)"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", jobs.id)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@title", jobs.title)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@minSalary", jobs.minSalary)); //command menambahkan parameter pada query

                    command.Parameters.Add(new SqlParameter("@maxSalary", jobs.maxSalary)); //command menambahkan parameter pada query

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

        public string Update(Jobs jobs)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"UPDATE {tableName} SET title = @title, min_salary = @minSalary, max_salary = @maxSalary WHERE id = @id)"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", jobs.id)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@title", jobs.title)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@minSalary", jobs.minSalary)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@maxSalary", jobs.maxSalary)); //command menambahkan parameter pada query

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
