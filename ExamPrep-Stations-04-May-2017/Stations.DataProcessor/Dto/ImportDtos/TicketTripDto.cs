namespace Stations.DataProcessor.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Trip")]
    public class TicketTripDto
    {
        [XmlElement()]
        [Required]
        public string OriginStation { get; set; }

        [XmlElement()]
        [Required]
        public string DestinationStation { get; set; }

        [XmlElement()]
        [Required]
        public string DepartureTime { get; set; }
    }
}