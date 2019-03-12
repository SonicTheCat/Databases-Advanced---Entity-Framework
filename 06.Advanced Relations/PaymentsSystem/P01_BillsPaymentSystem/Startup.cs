namespace P01_BillsPaymentSystem
{
    using P01_BillsPaymentSystem.Core;
    using P01_BillsPaymentSystem.Core.Contracts;
    using P01_BillsPaymentSystem.Core.IO;
    using P01_BillsPaymentSystem.Core.IO.Contracts;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Initializer;

    public class Startup
    {
        public static void Main()
        {
            using (var context = new BillsPaymentSystemContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Initialize.Seed(context);

                IReader reader = new ConsoleReader();
                IWriter writer = new ConsoleWriter();
                IEngine engine = new Engine(reader, writer, context);

                engine.Run();
            }
        }
    }
}