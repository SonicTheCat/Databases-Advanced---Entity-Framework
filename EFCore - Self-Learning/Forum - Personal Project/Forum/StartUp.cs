namespace Forum
{
    using ForumData;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new ForumDbContext())
            {
                /* Test RegistrationDate - Shadow Property */
                //Random random = new Random();

                //var users = context.Users.ToList();

                //foreach (var user in users)
                //{
                //    var seconds = random.Next(-100_000_000, -1);
                //    var randomDate = DateTime.Now.AddSeconds(seconds);

                //    context.Entry(user).Property("RegistrationDate").CurrentValue = randomDate;
                //}
                //context.SaveChanges();

                /* Order Posts by Shadow Property */
               var orderedUsers = context.Users
                    .OrderBy(user => EF.Property<DateTime>(user, "RegistrationDate"))
                    .ToList();

                foreach (var user in orderedUsers)
                {
                    Console.WriteLine(context.Entry(user).Property("RegistrationDate").CurrentValue);
                }

                /* Test LastUpdate - Shadow Property */
                //var post = context.Posts.Find(1);
                //post.Title = "Core za Facebook!";
                //context.SaveChanges();
            }
        }
    }
}