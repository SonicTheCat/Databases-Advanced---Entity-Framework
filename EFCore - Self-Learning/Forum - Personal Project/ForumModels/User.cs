namespace ForumModels
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
        }

        public User(int id, string username, string activityLavel)
            : base()
        {
            this.UserId = id; 
            this.Username = username;
            this.Status = activityLavel;
        }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Status { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}