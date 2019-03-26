namespace StudentsXML
{
    using System;
    using System.Xml.Serialization;

    [XmlType("student")]
    public class Student
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("gender")]
        public string Gender { get; set; }

        [XmlElement("birthdate")]
        public DateTime BirthDate { get; set; }

        [XmlElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("university")]
        public string University { get; set; }

        [XmlElement("speciality")]
        public string Speciality { get; set; }

        [XmlElement("facultyNumber")]
        public string FacultyNumber { get; set; }

        [XmlArrayItem("exam")]
        public Exam[] Exams { get; set; }
    }
}