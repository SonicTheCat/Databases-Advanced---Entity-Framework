namespace PetClinic.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class AnimalDto
    {
        [MinLength(3), MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MinLength(3), MaxLength(20)]
        [Required]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        public PassportDto Passport { get; set; }
    }
}