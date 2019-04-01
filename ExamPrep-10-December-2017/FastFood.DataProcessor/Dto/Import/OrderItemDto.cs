namespace FastFood.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;

    using System.Xml.Serialization;

    [XmlType("Item")]
    public class OrderItemDto
    {
        [Required]
        [MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}