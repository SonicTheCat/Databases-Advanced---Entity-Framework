namespace PetClinic.Dto.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class PassportDto
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]{7}[0-9]{3}$", ErrorMessage = "Invalid Serial Number!")]
        public string SerialNumber { get; set; }

        [Required]
        [RegularExpression(@"^(\+359)[0-9]{9}$|^(0)[0-9]{9}$", ErrorMessage = "Invalid PhoneNumber!")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string OwnerName { get; set; }

        [Required]
        public string RegistrationDate { get; set; }
    }
}