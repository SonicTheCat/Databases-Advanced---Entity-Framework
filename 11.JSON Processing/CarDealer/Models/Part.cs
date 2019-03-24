namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Part
    {
        [Key]
        public int PartId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<PartCars> PartCars { get; set; } = new List<PartCars>();
    }
}