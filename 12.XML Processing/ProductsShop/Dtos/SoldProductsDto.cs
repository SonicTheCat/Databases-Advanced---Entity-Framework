namespace ProductsShop.Dtos
{
    using System.Xml.Serialization;

    [XmlType("product")]
    public class SoldProductsDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}