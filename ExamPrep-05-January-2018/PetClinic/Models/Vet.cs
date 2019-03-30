namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        [MinLength(3), MaxLength(50)]
        [Required]
        public string Profession { get; set; }

        [Range(22, 65)]
        public int Age { get; set; }

        [RegularExpression(@"^(\+359)[0-9]{9}$|^(0)[0-9]{9}$", ErrorMessage = "Invalid PhoneNumber!")]
        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<Procedure> Procedures { get; set; } = new HashSet<Procedure>();

        public override bool Equals(object obj)
        {
            if (!(obj is Vet))
            {
                return false;
            }

            var vet = (Vet)obj;

            return this.PhoneNumber == vet.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return 1603624094 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
        }
    }
}