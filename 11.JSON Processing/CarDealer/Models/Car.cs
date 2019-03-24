namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Car
    {
        [Key]
        public int CarId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        [NotMapped]
        public decimal Price => this.PartCars.Sum(x => x.Part.Price); 

        public ICollection<PartCars> PartCars { get; set; } = new List<PartCars>();

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}