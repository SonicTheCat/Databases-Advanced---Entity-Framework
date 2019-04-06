namespace SoftJail.DataProcessor.ImportDto
{
    using SoftJail.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Officer")]
    public class OfficerDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(3), MaxLength(30)]
        public string FullName { get; set; }

        [XmlElement("Money")]
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        [XmlElement("Position")]
        [Required]
        public string Position { get; set; }

        [XmlElement("Weapon")]
        [Required]
        public string Weapon { get; set; }

        [XmlElement()]
        [Required]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public OfficerPrisonerDto[] Prisoners { get; set; }
    }
}