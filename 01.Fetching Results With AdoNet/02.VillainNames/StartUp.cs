using System;
using System.Data.SqlClient;
using CommonFiles;

namespace _02.VillainNames
{
    public class StartUp
    {
        public static void Main()
        {
            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();

                var query = Statments.ExerciseTwo();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var villain = (string)reader["Name"];
                        var minnionsCount = (int)reader["MinionsCount"];

                        Console.WriteLine(villain + " - " + minnionsCount);
                    }
                }
            }
        }
    }
}