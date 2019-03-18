namespace NonStopMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using NonStopMarket.Models.Enums;
    
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [NotMapped]
        public decimal Income => this.ProductsOrders.Sum(p => p.Product.Price); 

        public DateTime? Date { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<ProductOrder> ProductsOrders { get; set; } = new List<ProductOrder>(); 
    }
}