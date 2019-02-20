using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CommonFiles; 

namespace _05ChangeTownNamesCasing
{
    public class StartUp
    {
        public static void Main()
        {
            var country = Console.ReadLine();

            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();
                var reader = ReaderGetTowns(country, connection, transaction);

                if (!reader.HasRows)
                {
                    Console.WriteLine("No town names were affected.");
                    transaction.Rollback();
                    return;
                }
                reader.Close(); 

                var command = new SqlCommand(string.Format(Statments.townsToUpper, country), connection, transaction);
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " town names were affected.");

                reader = ReaderGetTowns(country, connection, transaction);
                var towns = new List<string>();
                while (reader.Read())
                {
                    towns.Add((string)reader[0]); 
                }

                Console.WriteLine("[" + string.Join(", ", towns) + "]");
                transaction.Commit(); 
            }
        }

        static SqlDataReader ReaderGetTowns(string country, SqlConnection connection, SqlTransaction tran)
        {
            var command = new SqlCommand(string.Format(Statments.townsInCountry, country), connection, tran);
            var reader = command.ExecuteReader();

            return reader;
        }
    }
}