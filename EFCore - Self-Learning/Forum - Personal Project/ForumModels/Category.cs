namespace ForumModels
{
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        public Category(int id, string name)
            : base()
        {
            this.CategoryId = id; 
            this.Name = name;
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}