namespace P01_StudentSystem
{
    using P01_StudentSystem.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new StudentSystemContext())
            {
                //context.Database.EnsureDeleted(); 

                //context.Database.EnsureCreated(); 
            }
        }
    }
}