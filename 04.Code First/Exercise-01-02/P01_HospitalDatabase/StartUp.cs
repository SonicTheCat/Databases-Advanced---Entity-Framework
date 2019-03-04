namespace P01_HospitalDatabase
{
    using P01_HospitalDatabase.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (HospitalContext context = new HospitalContext())
            {
                context.Database.EnsureDeleted();
               
              
            }
        }
    }
}
