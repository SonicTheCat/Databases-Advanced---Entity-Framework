namespace NonStopMarket.Data
{
    using System;
    using System.IO;

    public class Configuration
    {
        public static string ConnectionString()
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"C:\Users\Pavel\ConnectionString.txt");
            return File.ReadAllText(path);
        }
    }
}