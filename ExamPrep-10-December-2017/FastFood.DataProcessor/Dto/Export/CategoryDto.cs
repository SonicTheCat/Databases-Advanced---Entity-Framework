namespace FastFood.DataProcessor.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class CategoryDto
    {
        [XmlElement()]
        public string Name { get; set; }

        [XmlElement("MostPopularItem")]
        public MostPopularItemDto MostPopularItem { get; set; }
    }
}