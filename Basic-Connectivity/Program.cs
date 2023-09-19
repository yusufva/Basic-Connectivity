using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Basic_Connectivity
{
    internal class Program
    {
        static string connectionString = "Data Source=DESKTOP-A42IQOB;Database=db_hr_dts;Connect Timeout=30;Integrated Security=True";

        static void Main(string[] args)
        {
            //GetAllRegion();
            //InsertRegion("Jawa Tengah");
            //GetRegionById(11);
            //UpdateRegion(11, "Jawa Barat");
            DeleteRegion(12);
        }

        // GET ALL : Regions
        public static void GetAllRegion()
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM region";

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows){
                    //output the column headers
                    while (reader.Read()) {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                    }
                }
                else{
                    Console.WriteLine("No Rows found.");
                }

                reader.Close();
            } catch (Exception e){
                Console.WriteLine($"Errpr: {e.Message}");
            } finally{
                connection.Close();
            }
        }

        // GET BY ID : Region
        public static void GetRegionById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM region WHERE id = @id";
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(pId);

            try { 
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: "+ reader.GetString(1));
                    }
                }
                else
                {
                    Console.WriteLine($"Region with id: {id} not found");
                }

                reader.Close();
            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            } finally
            {
                connection.Close();
            }
        }

        // INSERT: Region
        public static void InsertRegion(string name)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            
            command.Connection = connection;
            command.CommandText = "INSERT INTO region VALUES (@name)";

            
            try
            {
                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlParameter pName = new SqlParameter();
                    pName.ParameterName = "Name";
                    pName.Value = name;
                    pName.SqlDbType = SqlDbType.VarChar;
                    command.Parameters.Add(pName);

                    command.Transaction = transaction;

                    int result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    switch (result)
                    {
                        case >=1:
                            Console.WriteLine("insert success");
                            break;
                        default:
                            Console.WriteLine("insert failed");
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
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE region SET name = @name WHERE id = @id";

            try
            {
                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlParameter pName = new SqlParameter();
                    pName.ParameterName = "name";
                    pName.Value = name;
                    pName.SqlDbType = SqlDbType.VarChar;
                    command.Parameters.Add(pName);

                    SqlParameter pId = new SqlParameter();
                    pId.ParameterName = "id";
                    pId.Value = id;
                    pId.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(pId);

                    command.Transaction = transaction;

                    int result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    switch (result)
                    {
                        case >= 1:
                            Console.WriteLine("Update success");
                            break;
                        default:
                            Console.WriteLine("Update failed");
                            break;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error transaction: {e.Message}");
                }

            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        // Delete : Region
        public static void DeleteRegion(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "DELETE FROM region WHERE id = @id";

            try
            {
                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlParameter pId = new SqlParameter();
                    pId.ParameterName = "id";
                    pId.Value = id;
                    pId.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(pId);

                    command.Transaction = transaction;

                    int result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    switch (result)
                    {
                        case >= 1:
                            Console.WriteLine("Delete success");
                            break;
                        default:
                            Console.WriteLine("Delete failed");
                            break;
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error transaction: {e.Message}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}