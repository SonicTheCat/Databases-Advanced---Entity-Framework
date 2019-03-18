namespace NonStopMarket.App
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Core;
    using Core.Contracts;
    using NonStopMarket.Data;
    using NonStopMarket.Services;
    using NonStopMarket.Services.Contracts;
    using AutoMapper;
    using Core.IO;
    using Core.IO.Contracts;

    public class Startup
    {
        static void Main()
        {
            IServiceProvider services = ConfigureServices();
            IEngine engine = new Engine(services);

            engine.Run(); 
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<NonStopMarketContext>(opt => opt.UseSqlServer(Configuration.ConnectionString()));

            services.AddAutoMapper(config => config.AddProfile<NonStopMarketProfile>()); 

            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddTransient<ICommandInterpreter, CommandInterpreter>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IReader, ConsoleReader>();
            services.AddTransient<IWriter, ConsoleWriter>();

            return services.BuildServiceProvider(); 
        }
    }
}