namespace P01_StudentSystem.Data.Paths
{
    using System;
    using System.IO;

    public class DataPath
    {
        public static string StudentsPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Students.txt");

        public static string CoursesPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Courses.txt");

        public static string HomeworkdsPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Homeworks.txt");

        public static string ResourcesPath => Path.Combine(Environment.CurrentDirectory, @"C:\Users\Petya\Desktop\SoftUniPavel\Resources.txt");
    }
}