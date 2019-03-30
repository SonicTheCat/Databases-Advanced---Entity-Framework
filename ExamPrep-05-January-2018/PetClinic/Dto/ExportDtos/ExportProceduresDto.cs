namespace PetClinic.Dto.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ExportProceduresDto
    {
        [XmlElement()]
        public string Passport { get; set; }

        [XmlElement()]
        public string OwnerNumber { get; set; }

        [XmlElement()]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public ExportAidsDto[] AnimalAids{ get; set; }

        [XmlElement()]
        public decimal TotalPrice { get; set; }
    }
}