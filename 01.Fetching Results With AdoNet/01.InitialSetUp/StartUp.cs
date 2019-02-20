using System;
using System.Data.SqlClient;
using CommonFiles;

namespace _01.InitialSetUp
{
    public class StartUp
    {
        public static void Main()
        {
            var connParams = "Server=.; Integrated Security=true;";

            using (var connection = new SqlConnection(connParams))
            {
                connection.Open();

                var querry = "create database MinionsDB";
                TryExecute(querry, connection);
            }

            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();
                var queries = Statments.CreateStatments();
                foreach (var querry in queries)
                {
                    TryExecute(querry, connection);
                }
            }
        }

        public static void TryExecute(string querry, SqlConnection connection)
        {
            try
            {
                using (var command = new SqlCommand(querry, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}