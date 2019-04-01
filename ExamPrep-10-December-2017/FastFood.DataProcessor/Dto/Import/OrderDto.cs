namespace FastFood.DataProcessor.Dto.Import
{
    using FastFood.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Order")]
    public class OrderDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string Employee { get; set; }

        public string DateTime { get; set; }

        public OrderType Type { get; set; } = OrderType.ForHere;

        [XmlArray("Items")]
        public OrderItemDto[] Items{ get; set; }
    }
}