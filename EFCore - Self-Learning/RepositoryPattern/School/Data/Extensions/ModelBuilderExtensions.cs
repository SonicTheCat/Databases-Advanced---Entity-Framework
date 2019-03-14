using Microsoft.EntityFrameworkCore;
using SoftUni.Models;
using System.Collections.Generic;

namespace SoftUni.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            #region Add Tags
            var tags = new Tag[]
            {
                new Tag {Id = 1, Name = "c#"},
                new Tag {Id = 2, Name = "angularjs"},
                new Tag {Id = 3, Name = "javascript"},
                new Tag {Id = 4, Name = "nodejs"},
                new Tag {Id = 5, Name = "oop"},
                new Tag {Id = 6, Name = "linq"}
            };

            builder.Entity<Tag>().HasData(tags);
            #endregion

            #region Add Teachers
            var teachers = new Teacher[]
            {
                new Teacher
                {
                    Id = 1,
                    Name = "Mosh Hamedani"
                },
                new Teacher
                {
                    Id = 2,
                    Name = "Anthony Alicea"
                },
                new Teacher
                {
                    Id = 3,
                    Name = "Eric Wise"
                },
                new Teacher
                {
                    Id = 4,
                    Name = "Tom Owsiak"
                },
                new Teacher
                {
                    Id = 5,
                    Name = "John Smith"
                }
            };

            builder.Entity<Teacher>().HasData(teachers);
            #endregion

            #region Add Courses
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "C# Basics",
                    TeacherId = 1,
                    FullPrice = 49,
                    Description = "Description for C# Basics",
                    Level = 1
                },
                new Course
                {
                    Id = 2,
                    Name = "C# Intermediate",
                    TeacherId = 4,
                    FullPrice = 49,
                    Description = "Description for C# Intermediate",
                    Level = 2
                },
                new Course
                {
                    Id = 3,
                    Name = "C# Advanced",
                    TeacherId = 5,
                    FullPrice = 69,
                    Description = "Description for C# Advanced",
                    Level = 3
                },
                new Course
                {
                    Id = 4,
                    Name = "Javascript: Understanding the Weird Parts",
                    TeacherId = 1,
                    FullPrice = 149,
                    Description = "Description for Javascript",
                    Level = 2
                },
                new Course
                {
                    Id = 5,
                    Name = "Learn and Understand AngularJS",
                    TeacherId = 3,
                    FullPrice = 99,
                    Description = "Description for AngularJS",
                    Level = 2
                },
                new Course
                {
                    Id = 6,
                    Name = "Learn and Understand NodeJS",
                    TeacherId = 2,
                    FullPrice = 149,
                    Description = "Description for NodeJS",
                    Level = 2
                },
                new Course
                {
                    Id = 7,
                    Name = "Programming for Complete Beginners",
                    TeacherId = 1,
                    FullPrice = 45,
                    Description = "Description for Programming for Beginners",
                    Level = 1
                },
                new Course
                {
                    Id = 8,
                    Name = "A 16 Hour C# Course with Visual Studio 2013",
                    TeacherId = 3,
                    FullPrice = 150,
                    Description = "Description 16 Hour Course",
                    Level = 1
                },
                new Course
                {
                    Id = 9,
                    Name = "Learn JavaScript Through Visual Studio 2013",
                    TeacherId = 4,
                    FullPrice = 20,
                    Description = "Description Learn Javascript",
                    Level = 1
                }
            };

            builder.Entity<Course>().HasData(courses);
            #endregion
        }
    }
}