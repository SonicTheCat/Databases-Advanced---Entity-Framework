namespace Stations.DataProcessor.Dto.ImportDtos
{
    using Stations.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Card")]
    public class CardDto
    {
        [Required]
        [MaxLength(128)]
        [XmlElement()]
        public string Name { get; set; }

        [Range(0, 120)]
        [XmlElement()]
        public int Age { get; set; }

        [XmlElement("CardType")]
        public CardType? Type { get; set; } = CardType.Normal;
    }
}