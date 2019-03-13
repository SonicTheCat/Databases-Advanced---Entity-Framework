namespace P01_BillsPaymentSystem
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using P01_BillsPaymentSystem.Core;
    using P01_BillsPaymentSystem.Core.Contracts;
    using P01_BillsPaymentSystem.Core.IO;
    using P01_BillsPaymentSystem.Core.IO.Contracts;
    using P01_BillsPaymentSystem.Data;

    public class Startup
    {
        public static void Main()
        {
            using (var context = new BillsPaymentSystemContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                //Initialize.Seed(context);

                using (var unitOfWork = new UnitOfWork(context))
                {
                    IServiceProvider serviceProvider = ConfigureServices(unitOfWork);
                    ICommandInterpreter commandInterpreter = new CommandInterpreter(serviceProvider);

                    IReader reader = new ConsoleReader();
                    IWriter writer = new ConsoleWriter();
                    IEngine engine = new Engine(reader, writer, commandInterpreter);

                    engine.Run();
                }
            }       
        }

        private static IServiceProvider ConfigureServices(IUnitOfWork unitOfWork)
        {
            IServiceCollection services = new ServiceCollection();

            //IUnitOfWork unitOfWork = new UnitOfWork(new BillsPaymentSystemContext());
            services.AddSingleton(unitOfWork);

            services.AddTransient<IReader, ConsoleReader>(); 
            services.AddTransient<IWriter, ConsoleWriter>(); 

            return services.BuildServiceProvider(); 
        }
    }
}