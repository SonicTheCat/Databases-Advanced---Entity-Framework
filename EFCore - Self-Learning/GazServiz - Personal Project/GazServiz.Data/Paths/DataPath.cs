namespace GazServiz.Data.Paths
{
    using System;
    using System.IO;

    public class DataPath
    {
        public static string OwnersPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Owners.txt");

        public static string EmployeesPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Employees.txt");

        public static string CarsPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Cars.txt");

        public static string RepairsPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Repairs.txt");
    }
}