namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TrainSeat
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }
        [Required]
        public Train Train { get; set; }

        [ForeignKey(nameof(SeatingClass))]
        public int SeatingClassId { get; set; }
        [Required]
        public SeatingClass SeatingClass { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}