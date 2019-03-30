namespace PetClinic.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Vet")]
    public class VetDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("Profession")]
        [MinLength(3), MaxLength(50)]
        [Required]
        public string Profession { get; set; }

        [XmlElement("Age")]
        [Range(22, 65)]
        public int Age { get; set; }

        [XmlElement("PhoneNumber")]
        [RegularExpression(@"^(\+359)[0-9]{9}$|^(0)[0-9]{9}$", ErrorMessage = "Invalid PhoneNumber!")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}