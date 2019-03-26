namespace ProductsShop.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("category")]
    public class CategoryDto
    {
        [XmlElement("name")]
        [MinLength(3)]
        public string Name { get; set; }
    }
}