namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ticket
    {
        public int Id { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [ForeignKey(nameof(Projection))]
        public int ProjectionId { get; set; }
        public Projection Projection { get; set; }
    }
}