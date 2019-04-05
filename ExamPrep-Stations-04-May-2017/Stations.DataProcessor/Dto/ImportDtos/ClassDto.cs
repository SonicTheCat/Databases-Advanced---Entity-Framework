namespace Stations.DataProcessor.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class ClassDto
    {
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^\s]{2}$")]
        public string Abbreviation { get; set; }
    }
}