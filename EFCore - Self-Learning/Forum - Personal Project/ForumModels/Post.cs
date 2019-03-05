namespace ForumModels
{
    using System.Collections.Generic;

    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>(); 
        }

        public Post(int id, string title, string content, int userId, int categoryId)
            :base()
        {
            this.PostId = id;
            this.Title = title;
            this.Content = content;
            this.AuthorId = userId;
            this.CategoryId = categoryId; 
        }

        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}