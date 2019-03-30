namespace PetClinic.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class AnimalAidsDto
    {
        [XmlElement("Name")]
        [MinLength(3), MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}