namespace ForumModels
{
    public class Comment
    {
        public Comment()
        {
        }

        public Comment(int id, string content, int userId, int postId)
        {
            this.CommentId = id; 
            this.Content = content;
            this.AuthorId = userId;
            this.PostId = postId; 
        }

        public int CommentId { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}