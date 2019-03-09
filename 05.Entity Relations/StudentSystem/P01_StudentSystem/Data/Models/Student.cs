namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        public Student(int studentId, string name, string phoneNumber, DateTime registeredOn)
        {
            StudentId = studentId;
            Name = name;
            PhoneNumber = phoneNumber;
            RegisteredOn = registeredOn;
        }

        public int StudentId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }

        public ICollection<StudentCourse> CourseEnrollments  { get; set; }
    }
}