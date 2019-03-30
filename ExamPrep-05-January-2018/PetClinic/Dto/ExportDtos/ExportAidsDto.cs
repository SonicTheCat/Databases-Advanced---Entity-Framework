namespace PetClinic.Dto.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class ExportAidsDto
    {
        [XmlElement()]
        public string Name { get; set; }

        [XmlElement()]
        public decimal Price { get; set; }
    }
}