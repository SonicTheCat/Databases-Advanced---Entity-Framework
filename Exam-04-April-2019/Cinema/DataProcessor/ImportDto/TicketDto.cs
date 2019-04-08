namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketDto
    {
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ProjectionId { get; set; }
    }
}