namespace ProductsShop.Models
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<CategoryProducts> CategoryProducts { get; set; } = new List<CategoryProducts>(); 
    }
}