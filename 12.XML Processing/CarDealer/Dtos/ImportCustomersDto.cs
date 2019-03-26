namespace CarDealer.Dtos
{
    using System;
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class ImportCustomersDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("birth-date")]
        public DateTime BirthDate { get; set; }

        [XmlElement("is-young-driver")]
        public bool IsYoungDriver { get; set; }
    }
}