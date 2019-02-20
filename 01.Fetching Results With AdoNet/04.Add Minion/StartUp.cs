using System;
using System.Data.SqlClient;
using CommonFiles; 

namespace Add_Minion
{
    public class StartUp
    {
        public static void Main()
        {
            var minionTokens = Console.ReadLine().Split();
            var villainName = Console.ReadLine().Split()[1];

            var minionName = minionTokens[1];
            var age = int.Parse(minionTokens[2]);
            var town = minionTokens[3];

            using (var connection = new SqlConnection(Configuration.connectionParams))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    var townId = ExecuteScalar(string.Format(Statments.getTown, town), connection);
                    if (townId is null)
                    {
                        ExecuteNonQuerry(string.Format(Statments.insertTown, town), connection);
                        Print($"Town {town} was added to the database.");
                        townId = ExecuteScalar(string.Format(Statments.getTown, town), connection);
                    }

                    var villainId = ExecuteScalar(string.Format(Statments.getVillain, villainName), connection);
                    if (villainId is null)
                    {
                        ExecuteNonQuerry(string.Format(Statments.insertVillain, villainName), connection);
                        villainId = ExecuteScalar(string.Format(Statments.getVillain, villainName), connection);
                        Print($"Villain {villainName} was added to the database.");
                    }

                    ExecuteNonQuerry(string.Format(Statments.insertMinion, minionName, age, townId), connection);
                    var minionId = ExecuteScalar(string.Format(Statments.getMinion, minionName), connection);
                    ExecuteNonQuerry(string.Format(Statments.insertMinionAndVillain, minionId, villainId), connection);

                    Print($"Successfully added {minionName} to be minion of {villainName}.");

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        static void Print(string text) => Console.WriteLine(text);

        static object ExecuteScalar(string query, SqlConnection connection)
        {
            return new SqlCommand(query, connection).ExecuteScalar();
        }

        static int ExecuteNonQuerry(string query, SqlConnection connection)
        {
            return new SqlCommand(query, connection).ExecuteNonQuery();
        }
    }
}