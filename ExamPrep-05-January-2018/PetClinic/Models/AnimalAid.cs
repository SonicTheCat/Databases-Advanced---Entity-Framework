namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AnimalAid
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public ICollection<ProcedureAnimalAid> AnimalAidProcedures { get; set; } = new HashSet<ProcedureAnimalAid>();

        public override bool Equals(object obj)
        {
            if (!(obj is AnimalAid))
            {
                return false; 
            }

            var aid = (AnimalAid)obj;

            return this.Name == aid.Name; 
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}