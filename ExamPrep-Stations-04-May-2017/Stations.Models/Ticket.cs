namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[^\s]{2}[0-9]{1,6}$")]
        [Required]
        public string SeatingPlace { get; set; }

        [ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        [Required]
        public Trip Trip { get; set; }

        [ForeignKey(nameof(CustomerCard))]
        public int? CustomerCardId { get; set; }
        public CustomerCard CustomerCard { get; set; }
    }
}