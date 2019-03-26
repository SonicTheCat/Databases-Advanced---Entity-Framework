namespace CarDealer.Dtos
{
    using System.Xml.Serialization;

    [XmlType("part")]
    public class ImportPartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("quantity")]
        public int Quantity { get; set; }
    }
}