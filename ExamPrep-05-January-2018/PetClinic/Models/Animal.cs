namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MinLength(3), MaxLength(20)]
        [Required]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public string PassportSerialNumber { get; set; }
        public Passport Passport { get; set; }

        public ICollection<Procedure> Procedures { get; set; } = new HashSet<Procedure>();

        public override bool Equals(object obj)
        {
            if (!(obj is Animal))
            {
                return false;
            }

            var animal = (Animal)obj;

            return this.Passport.SerialNumber == animal.Passport.SerialNumber;
        }

        public override int GetHashCode()
        {
            return -2122110570 + EqualityComparer<string>.Default.GetHashCode(PassportSerialNumber);
        }
    }
}