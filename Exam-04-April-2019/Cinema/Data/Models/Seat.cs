namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Seat
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }
        public Hall Hall { get; set; }
    }
}