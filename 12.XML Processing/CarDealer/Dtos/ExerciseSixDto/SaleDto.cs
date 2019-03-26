namespace CarDealer.Dtos.ExerciseSixDto
{
    using System.Xml.Serialization;

    [XmlType("sale")]
    public class SaleDto
    {
        [XmlElement("car")]
        public SaleCarDto Carr { get; set; }

        [XmlElement("customer-name")]
        public string FullName { get; set; }

        [XmlElement("discount")]
        public double Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWothDiscount { get; set; }
    }
}