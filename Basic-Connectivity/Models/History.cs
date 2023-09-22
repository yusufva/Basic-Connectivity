using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Models
{
    public class History
    {
        Connect connectionString = new Connect(); //mengambil connection String
        private static string tableName = "histories"; //deklarasi nama tabel

        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int employeeId { get; set; }
        public int deparmentId { get; set; }
        public string jobId { get; set; }

        public override string ToString()
        {
            return $"{startDate} - {endDate} - {employeeId} - {deparmentId} - {jobId}";
        }

        public History()
        {
            startDate = DateTime.Now;
            endDate = DateTime.Now;
            employeeId = 0;
            deparmentId = 0;
            jobId = string.Empty;
        }

        public History(DateTime startDate, DateTime endDate, int employeeId, int deparmentId, string jobId)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.employeeId = employeeId;
            this.deparmentId = deparmentId;
            this.jobId = jobId;
        }

        public string Delete(DateTime date)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"DELETE FROM {tableName} WHERE start_date = @startDate"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@startDate", date)); //command menambahkan parameter ke dalam sql query

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

        public List<History> GetAll()
        {
            var histories = new List<History>();

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
                        histories.Add(new History
                        {
                            startDate = reader.GetDateTime(0),
                            employeeId = reader.GetInt32(1),
                            endDate = reader.GetDateTime(2),
                            deparmentId = reader.GetInt32(3),
                            jobId = reader.GetString(4)
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return histories;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errpr: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<History>();
        }

        public List<History> GetById(int id)
        {
            var histories = new List<History>();

            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db
            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"SELECT * FROM {tableName} WHERE employee_id = @id"; //command sql query yang akan dijalankan
            command.Parameters.Add(new SqlParameter("@id", id)); //command menambahkan parameter ke dalam sql query

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
                        histories.Add(new History
                        {
                            startDate = reader.GetDateTime(0),
                            employeeId = reader.GetInt32(1),
                            endDate = reader.GetDateTime(2),
                            deparmentId = reader.GetInt32(3),
                            jobId = reader.GetString(4)
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return histories;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<History>();
        }

        public string Insert(History history)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"INSERT INTO {tableName} VALUES (@startDate, @employeeId, @endDate, @departmentId, @jobId)"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@startDate", history.startDate)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@employeeId", history.employeeId)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@endDate", history.endDate)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@departmentId", history.deparmentId)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@jobId", history.jobId)); //command menambahkan parameter pada query

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

        public string Update(History history)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"UPDATE {tableName} SET end_date = @endDate WHERE employee_id = @employeeId AND start_date = @startDate)"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@startDate", history.startDate)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@employeeId", history.employeeId)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@endDate", history.endDate)); //command menambahkan parameter pada query

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
