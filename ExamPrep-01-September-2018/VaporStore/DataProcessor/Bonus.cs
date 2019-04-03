namespace VaporStore.DataProcessor
{
	using System.Linq;
    using Data;

	public static class Bonus
	{
        public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return $"User {username} not found";
            }

            var isTaken = context.Users.FirstOrDefault(x => x.Email == newEmail) != null;
            if (isTaken)
            {
                return $"Email {newEmail} is already taken";
            }

            user.Email = newEmail;

            context.SaveChanges();

            return $"Changed {username}'s email successfully";
        }
	}
}