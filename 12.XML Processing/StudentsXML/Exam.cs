namespace StudentsXML
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("exams")]
    public class Exam
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("dateTaken")]
        public DateTime DateTaken { get; set; }

        [XmlElement("grade")]
        public double Grade { get; set; }
    }
}