namespace NonStopMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public int TotalQuantity { get; set; }

        public int QuantityInstock { get; set; }

        [NotMapped]
        public int SoldAmount => this.TotalQuantity - this.QuantityInstock;

        public decimal Price { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public ICollection<ProductOrder> ProductsOrders { get; set; } = new List<ProductOrder>();
    }
}