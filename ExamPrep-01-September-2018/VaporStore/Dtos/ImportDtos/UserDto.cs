namespace VaporStore.Dtos.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [MinLength(3), MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Za-z]* [A-Z]{1}[A-Za-z]*$")]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(3, 103)]
        public int Age { get; set; }

        public ICollection<CardDto> Cards { get; set; } = new List<CardDto>();
    }
}