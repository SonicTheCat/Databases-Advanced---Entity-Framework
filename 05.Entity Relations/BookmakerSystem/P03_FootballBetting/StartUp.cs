namespace P03_FootballBetting
{
    using P03_FootballBetting.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new FootballBettingContext())
            {
                context.Database.EnsureDeleted();

                context.Database.EnsureCreated(); 
            }
        }
    }
}