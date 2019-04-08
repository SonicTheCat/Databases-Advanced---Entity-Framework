namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class CustomerDto
    {
        [MinLength(3), MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(20)]
        [Required]
        public string LastName { get; set; }

        [Range(12, 110)]
        [Required]
        public int Age { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public TicketDto[] Tickets { get; set; }
    }
}