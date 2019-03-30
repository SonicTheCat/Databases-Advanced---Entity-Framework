namespace PetClinic.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [XmlElement("Animal")]
        [Required]
        public string Animal { get; set; }

        [XmlElement("Vet")]
        [Required]
        public string Vet { get; set; }

        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidsDto[] ProcedureAnimalAids { get; set; }
    }
}