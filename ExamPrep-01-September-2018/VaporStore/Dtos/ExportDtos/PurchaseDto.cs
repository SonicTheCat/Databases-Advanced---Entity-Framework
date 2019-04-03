namespace VaporStore.Dtos.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class PurchaseDto
    {
        [XmlElement("Card")]
        public string Card { get; set; }

        [XmlElement("Cvc")]
        public string Cvc { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("Game")]
        public GameDto Game{ get; set; }
    }
}