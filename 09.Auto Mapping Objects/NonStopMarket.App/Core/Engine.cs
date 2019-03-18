namespace NonStopMarket.App.Core
{
    using System.Linq;
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Contracts;
    using NonStopMarket.Services.Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.reader = this.serviceProvider.GetService<IReader>();
            this.writer = this.serviceProvider.GetService<IWriter>();
        }

        public void Run()
        {
            this.writer.WriteLine(Messagess.InitializingDatabase);

            var initializer = this.serviceProvider.GetService<IDbInitializer>();
            initializer.InitializeDatabase();

            var commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();

            this.writer.WriteLine(Messagess.ShowCommands());

            while (true)
            {
                try
                {
                    var input = this.reader
                   .ReadLine()
                   .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    this.writer.WriteLine(Messagess.Loading);

                    var command = commandInterpreter.InterpretCommand(input);

                    var data = input.Skip(1).ToArray();
                    var result = command.Execute(data);

                    this.writer.WriteLine(result);
                }
                catch (NullReferenceException)
                {
                    this.writer.WriteLine(Messagess.InvalidCommand);

                }
                catch (ArgumentException ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}