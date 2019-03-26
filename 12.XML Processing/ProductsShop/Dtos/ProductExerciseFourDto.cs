using System.Xml.Serialization;

namespace ProductsShop.Dtos
{
    [XmlType("sold-products")]
    public class ProductExerciseFourDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public SoldProductExerciseFourDto[] SoldProducts { get; set; }
    }
}