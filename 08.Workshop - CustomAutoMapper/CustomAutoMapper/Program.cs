using System;
using System.Collections.Generic;

namespace CustomAutoMapper
{
    class Program
    {
        static void Main()
        {
            var mapper = new Mapper();

            var person = new Person()
            {
                Name = "Asan",
                Age = 20,
                Courses = new List<string>() { "c#", "js" }
            };

            var student = mapper.CreateMappedObject<Student>(person);
            Console.WriteLine();
        }
    }
}