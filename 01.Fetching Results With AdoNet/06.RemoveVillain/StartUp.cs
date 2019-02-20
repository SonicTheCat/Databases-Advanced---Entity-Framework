using System;
using System.Data.SqlClient;
using CommonFiles;

namespace _06.RemoveVillain
{
    public class StartUp
    {
        public static void Main()
        {
            var villainId = int.Parse(Console.ReadLine());

            using (var conn = new SqlConnection(Configuration.connectionParams))
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                var command = new SqlCommand(string.Format(Statments.MinionsCountQuery, villainId), conn, tran);
                var releasedMinions = (int)command.ExecuteScalar();

                command = new SqlCommand(string.Format(Statments.VillainNameQ, villainId), conn, tran);
                var name = (string)command.ExecuteScalar();
                if (name is null)
                {
                    Console.WriteLine("No such villain was found.");
                    tran.Rollback();
                    return; 
                }

                try
                {                                 
                    TryExecuteCommand(conn, tran, string.Format(Statments.DeleteFromTable, "MinionsVillains", "VillainId", villainId));                             
                    TryExecuteCommand(conn, tran, string.Format(Statments.DeleteFromTable, "Villains", "Id", villainId));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    tran.Rollback();
                    return; 
                }

                Console.WriteLine($"{name} was deleted.");
                Console.WriteLine(releasedMinions +  " minions were released.");

                tran.Commit(); 
            }
        }

        public static void TryExecuteCommand(SqlConnection conn, SqlTransaction tran, string commandString)
        {
            var command = new SqlCommand(commandString, conn, tran);
            command.ExecuteNonQuery(); 
        }
    }
}