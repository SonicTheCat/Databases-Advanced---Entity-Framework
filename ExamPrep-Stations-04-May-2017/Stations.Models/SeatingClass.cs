namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SeatingClass
    {
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^\s]{2}$")]
        public string Abbreviation { get; set; }
    }
}