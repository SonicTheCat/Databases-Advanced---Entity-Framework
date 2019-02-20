using System;
using CommonFiles;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace _07.PrintAllMinionNames
{
    public class Program
    {
        public static void Main()
        {
            var minionsNames = new List<string>(); 
            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();
                using (var command = new SqlCommand(Statments.GetMinionsNames, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            minionsNames.Add((string)reader[0]); 
                        }
                    }
                }
            }

            var first = 0;
            var last = minionsNames.Count - 1;
            for (int i = 0; i < minionsNames.Count / 2; i++)
            {
                Console.WriteLine(minionsNames[first++]);
                Console.WriteLine(minionsNames[last--]);
            }

            if (minionsNames.Count % 2 != 0 && first == minionsNames.Count / 2)
            {
                Console.WriteLine(minionsNames[first]);
            }
        }
    }
}
