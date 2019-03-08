namespace GazServiz.Data.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using GazServiz.Models;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using GazServiz.Data.Paths;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            GetData<Owner>(DataPath.OwnersPath, builder);
            GetData<Employee>(DataPath.EmployeesPath, builder);
            GetData<Car>(DataPath.CarsPath, builder);
            GetData<Repair>(DataPath.RepairsPath, builder);
        }

        private static void GetData<T>(string path, ModelBuilder builder)
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