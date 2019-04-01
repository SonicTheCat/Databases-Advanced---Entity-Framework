namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; } = new HashSet<Item>();

        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Category))
        //    {
        //        return false;
        //    }

        //    var category = (Category)obj;

        //    return this.Name == category.Name;
        //}

        //public override int GetHashCode()
        //{
        //    return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        //}
    }
}