namespace Stations.DataProcessor.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class TrainSeatDto
    {
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^\s]{2}$")]
        public string Abbreviation { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}