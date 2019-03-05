namespace ForumData.Extensions
{
    using ForumModels;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(GetUsers());
            builder.Entity<Post>().HasData(GetPosts());
            builder.Entity<Comment>().HasData(GetComments());
            builder.Entity<Category>().HasData(GetCategories());
        }

        private static User[] GetUsers()
        {
            return new User[]
            {
                new User(1, "Mark Zukenberg", "Top User"),
                new User(2, "Jay-Z", "New"),
                new User(3,"Beyonce", "Star"),
                new User(4, "Lili Ivanova", "Vintage"),
                new User(5, "Kichka Bodurova", "Unknown")
            };
        }

        private static Post[] GetPosts()
        {
            var users = GetUsers();
            var categories = GetCategories();

            return new Post[]
            {
               new Post(1,"New Privacy Policy in FB", "Please read this message till the end! blq blq blq...", users[0].UserId, categories[1].CategoryId),
               new Post(2, "!!!NEW SINGLE!!!", "Listen to my brand new single - 'Самотни лелки' ", users[2].UserId, categories[0].CategoryId),
               new Post(3, "300 Години на Сцена", "Примата на българската естрада организира грандиозен конц...", users[3].UserId, categories[0].CategoryId),
            };
        }

        private static Category[] GetCategories()
        {
            return new Category[]
            {
                new Category(1,"Music"),
                new Category(2,"Infromation")
            };
        }

        private static Comment[] GetComments()
        {
            var users = GetUsers();
            var posts = GetPosts();

            return new Comment[]
            {
                new Comment(1,"tam sam!", users[1].UserId, posts[2].CategoryId),
                new Comment(2,"qko heit...", users[4].UserId, posts[2].CategoryId),
                new Comment(3,"kolko struva ?", users[0].UserId, posts[1].CategoryId),
                new Comment(4,"bez pari e we, kaltak ! Teaser", users[2].UserId, posts[1].CategoryId),
            };
        }
    }
}