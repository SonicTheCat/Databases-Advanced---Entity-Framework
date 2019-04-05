namespace Stations.DataProcessor.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketDto
    {
        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[^\s]{2}[0-9]{1,6}$")]
        [Required]
        [XmlAttribute("seat")]
        public string SeatingPlace { get; set; }

        [Required]
        [XmlElement("Trip")]
        public TicketTripDto Trip { get; set; }

        [XmlElement("Card")]
        public TicketCardDto Card { get; set; }
    }
}