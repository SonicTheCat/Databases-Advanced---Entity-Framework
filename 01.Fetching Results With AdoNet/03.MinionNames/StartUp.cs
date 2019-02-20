using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonFiles; 

namespace _03.MinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var id = int.Parse(Console.ReadLine());
            var minions = new List<Minion>();

            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();

                var reader = ExecuteQuerry(string.Format(Statments.VillainQuerry, id), connection);
                while (reader.Read())
                {
                    var name = (string)reader["Name"];
                    Print(string.Format(Statments.VillainName, name));
                }

                if (!reader.HasRows)
                {
                    Print(string.Format(Statments.NoVillain, id));
                    return; 
                }
                reader.Close(); 

                reader = ExecuteQuerry(string.Format(Statments.GetMinionsQuerry, id) , connection);
                while (reader.Read())
                {
                    var minionName = (string)reader["Name"];
                    var minionAge = (int)reader["Age"];

                    minions.Add(new Minion(minionName, minionAge)); 
                }

                var rowCounter = 0; 
                foreach (var minion in minions.OrderBy(x => x.Name).ThenByDescending(x => x.Age))
                {
                    Print(++rowCounter + ". " + minion); 
                }

                if (!reader.HasRows)
                {
                    Print(Statments.NoMinions);
                }
            }
        }

        static SqlDataReader ExecuteQuerry(string querry, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(querry, connection);
            return command.ExecuteReader();
        }

        static void Print(string text)
        {
            Console.WriteLine(text);
        }
    }
}