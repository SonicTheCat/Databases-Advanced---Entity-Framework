namespace Stations.DataProcessor.Dto.ExportDtos
{
    using Stations.Models.Enums;
    using System.Xml.Serialization;

    [XmlType("Card")]
    public class CardDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public CardType Type { get; set; }

        [XmlArray("Tickets")]
        public TicketDto[] Tickets{ get; set; }
    }
}