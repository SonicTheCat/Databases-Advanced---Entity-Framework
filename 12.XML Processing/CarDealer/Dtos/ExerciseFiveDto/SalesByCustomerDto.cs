namespace CarDealer.Dtos.ExerciseFiveDto
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class SalesByCustomerDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCarsCount { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}