namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> SellingProducts { get; set; } = new List<Product>();

        public ICollection<Product> BoughtProducts { get; set; } = new List<Product>();
    }
}