namespace FastFood.DataProcessor.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("MostPopularItemDto")]
    public class MostPopularItemDto
    {
        public string Name { get; set; }

        public decimal TotalMade { get; set; }

        public int TimesSold { get; set; }
    }
}