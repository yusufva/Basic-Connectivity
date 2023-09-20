using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public class Employee : Interface<Employee, int>
    {
        Connect connectionString = new Connect();
        private static string tableName = "employees";

        public int id {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime hireDate { get; set; }
        public int? salary { get; set; }
        public decimal commisionPct { get; set; }
        public int? managerId { get; set; }
        public string jobId { get; set; }
        public int departmentId { get; set; }

        public override string ToString()
        {
            return $"{id} - {firstName} - {lastName} - {email} - {phoneNumber} - {hireDate} - {salary} - {commisionPct} - {managerId} - {jobId} - {departmentId}";
        }

        public Employee()
        {
            this.id = 0;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.email = string.Empty;
            this.phoneNumber = string.Empty;
            this.departmentId = 0;
            this.jobId = string.Empty;
            this.departmentId = 0;
        }

        public Employee(int id, string firstName, string lastName, string email, string phoneNumber, DateTime hireDate, int salary, decimal commisionPct, int managerId, string jobId, int departmentId)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.hireDate = hireDate;
            this.salary = salary;
            this.commisionPct = commisionPct;
            this.managerId = managerId;
            this.jobId = jobId;
            this.departmentId = departmentId;
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

        public List<Employee> GetAll()
        {
            var employees = new List<Employee>();

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
                        employees.Add(new Employee
                        {
                            id = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2),
                            email = reader.GetString(3),
                            phoneNumber = reader.GetString(4),
                            hireDate = reader.GetDateTime(5),
                            salary = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                            commisionPct = reader.GetDecimal(7),
                            managerId = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                            jobId = reader.GetString(9),
                            departmentId = reader.GetInt32(10),
                        });
                    }
                    reader.Close(); //menutup reader
                    connection.Close(); //menutup koneksi db

                    return employees;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new List<Employee>();
        }

        public Employee GetById(int id)
        {
            var employee = new Employee();

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
                        id = reader.GetInt32(0);
                        firstName = reader.GetString(1);
                        lastName = reader.GetString(2);
                        email = reader.GetString(3);
                        phoneNumber = reader.GetString(4);
                        hireDate = reader.GetDateTime(5);
                        salary = reader.GetInt32(6);
                        commisionPct = reader.GetInt32(7);
                        managerId = reader.GetInt32(8);
                        jobId = reader.GetString(9);
                        departmentId = reader.GetInt32(10);
                    }
                    reader.Close();
                    connection.Close();
                    return employee;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}"); //menampilkan pesan jika error
            }
            return new Employee();
        }

        public string Insert(Employee employee)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"INSERT INTO {tableName} VALUES (@id, @firstName, @lastName, @email, @phoneNumber, @hireDate, @salary, @commisionPct, @managerId, @jobId, @departmentId)"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", employee.id)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@firstName", employee.firstName)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@lastName", employee.lastName)); //command menambahkan parameter pada query

                    command.Parameters.Add(new SqlParameter("@email", employee.email)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@phoneNumber", employee.phoneNumber)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@hireDate", employee.hireDate)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@salary", employee.salary)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@commisionPct", employee.commisionPct)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@managerId", employee.managerId)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@jobId", employee.jobId)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@departmentId", employee.departmentId)); //command menambahkan parameter pada query

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

        public string Update(Employee employee)
        {
            using var connection = new SqlConnection(connectionString.ConnectionString); //variabel untuk koneksi database
            using var command = new SqlCommand(); //variabel command untuk menjalankan command ke db

            command.Connection = connection; //command koneksi ke database
            command.CommandText = $"UPDATE {tableName} SET salary = @salary, commision_pct = @commisionPct WHERE id = @employeeId)"; //command sql query yang akan dijalankan

            try
            {
                connection.Open(); //membuka koneksi ke db
                using SqlTransaction transaction = connection.BeginTransaction(); //mendefinisikan transaksi
                try
                {
                    command.Parameters.Add(new SqlParameter("@salary", employee.salary)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@employeeId", employee.id)); //command menambahkan parameter pada query
                    command.Parameters.Add(new SqlParameter("@commisionPct", employee.commisionPct)); //command menambahkan parameter pada query

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
