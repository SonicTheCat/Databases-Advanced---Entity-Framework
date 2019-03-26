namespace ProductsShop.Dtos
{
    using System.Xml.Serialization;

    [XmlRoot("users")]
    public class UsersDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UsersProductsDto[] Users{ get; set; }
    }
}