namespace P01_StudentSystem.Data.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using P01_StudentSystem.Data.Models;
    using P01_StudentSystem.Data.Paths;

    using System.Collections.Generic;
    using System.IO;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            GetDataFromFile<Student>(DataPath.StudentsPath, builder);
            GetDataFromFile<Course>(DataPath.CoursesPath, builder);
            GetDataFromFile<Homework>(DataPath.HomeworkdsPath, builder);
            GetDataFromFile<Resource>(DataPath.ResourcesPath, builder);
        }

        private static void GetDataFromFile<T>(string path, ModelBuilder builder)
            where T : class
        {
            var entities = new List<T>();
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                entities = JsonConvert.DeserializeObject<List<T>>(json);
            }

            builder.Entity<T>().HasData(entities);
        }
    }
}