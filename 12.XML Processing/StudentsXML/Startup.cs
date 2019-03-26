namespace StudentsXML
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    public class Startup
    {
        public static void Main()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Student[]), new XmlRootAttribute("studentss"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var students = GetStudents();

            using (TextWriter writer = new StreamWriter("../../../Students.xml"))
            {
                serializer.Serialize(writer, students, namespaces);
            }

            //using (TextReader reader = new StreamReader("../../../Students.xml"))
            //{
            //    var students = (Student[])serializer.Deserialize(reader);

            //    students[0].Name = "Cassandra"; 
            //}
        }

        public static Student[] GetStudents()
        {
            var exams = new Exam[]
            {
                new Exam()
                {
                    Name = "Biology",
                    DateTaken = DateTime.Parse("09-08-2015"),
                    Grade = 2.00
                },
                    new Exam()
                {
                    Name = "Math",
                    DateTaken = DateTime.Parse("09-08-2015"),
                    Grade = 3.00
                },
                    new Exam()
                {
                    Name = "English",
                    DateTaken = DateTime.Parse("09-08-2015"),
                    Grade = 3.10
                },
                new Exam()
                {
                    Name = "French",
                    DateTaken = DateTime.Parse("09-08-2015"),
                    Grade = 3.70
                }
            };

            var students = new Student[]
            {
                new Student()
                {
                    Name = "Minka Minkova",
                    Gender = "Female",
                    BirthDate = DateTime.Parse("03-02-1995"),
                    PhoneNumber = "+359192939293",
                    Email = "minka@hotmail.com",
                    University = "Oxford University",
                    Speciality = "Medicine",
                    FacultyNumber = "12313131391",
                    Exams = exams
                },

                 new Student()
                {
                    Name = "Ivan Ivanov",
                    Gender = "Male",
                    BirthDate = DateTime.Parse("09-08-1992"),
                    PhoneNumber = "+359192939293",
                    Email = "I.ivanov@gmai.io",
                    University = "Oxford University",
                    Speciality = "Kufrajist",
                    FacultyNumber = "12313131391",
                    Exams = exams
                },
            };

            return students;
        }
    }
}