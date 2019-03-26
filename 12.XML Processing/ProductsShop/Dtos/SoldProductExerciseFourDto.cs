using System.Xml.Serialization;

namespace ProductsShop.Dtos
{
    [XmlType("product")]
    public class SoldProductExerciseFourDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}