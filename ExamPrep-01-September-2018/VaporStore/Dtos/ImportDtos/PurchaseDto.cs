namespace VaporStore.Dtos.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using VaporStore.Data.Enums;

    [XmlType("Purchase")]
    public class PurchaseDto
    {
        [XmlElement("Type")]
        [Required]
        public PurchaseType Type { get; set; }

        [XmlElement("Key")]
        [Required]
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        public string ProductKey { get; set; }

        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }

        [XmlElement("Card")]
        [Required]
        [RegularExpression(@"^[0-9]{4}\s*[0-9]{4}\s*[0-9]{4}\s*[0-9]{4}$")]
        public string Card { get; set; }

        [XmlAttribute("title")]
        [Required]
        public string Game { get; set; }
    }
}