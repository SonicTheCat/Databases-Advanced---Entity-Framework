using System;
using System.Data;
using System.Data.SqlClient;
using CommonFiles;

namespace _09.IncreaseAgeStoredProcedure
{
    public class StartUp
    {
        public static void Main()
        {
            var id = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();
                using (var command = new SqlCommand(Statments.ProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                    {
                        Print("Something has gone wrong");
                        return;
                    }

                    Print("Rows Affected: " + rowsAffected);
                }

                using (var command = new SqlCommand(string.Format(Statments.UpdatedMinion, id), connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        Print(reader[0] + " - " + reader[1] + " years old");
                    }
                }
            }
        }

        static void Print(string text) => Console.WriteLine(text);
    }
}