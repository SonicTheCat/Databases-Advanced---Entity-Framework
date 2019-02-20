using System;
using CommonFiles;
using System.Data.SqlClient;
using System.Linq;

namespace _08.IncreaseMinionAge
{
    public class StartUp
    {
        public static void Main()
        {
            var minionsToUpdate = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();

                var cmdText = string.Format(Statments.UpdateMinions, string.Join(", ", minionsToUpdate));
                using (var command = new SqlCommand(cmdText, connection))
                {
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        Print("No minions found with the provided ids!");
                        return;
                    }
                    Print("Rows Affected: " + rowsAffected);
                }

                cmdText = string.Format(Statments.GetUpdatedValues, string.Join(", ", minionsToUpdate));
                using (var command = new SqlCommand(cmdText, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Print(reader[0] + " " + reader[1]);
                        }
                    }
                }
            }
        }

        static void Print(string text) => Console.WriteLine(text);
    }
}