namespace NonStopMarket.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using NonStopMarket.App.Core.Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "command"; 

        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IExecutable InterpretCommand(string[] data)
        {
            var inputCommand = data[0];
            
            var type = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(x => x.Name.ToLower() == inputCommand.ToLower() + Suffix)
                .SingleOrDefault();

            var commandConstructor = type
                .GetConstructors()
                .First();

            var constructorParams = commandConstructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var injectedParams = constructorParams
                .Select(serviceProvider.GetService)
                .ToArray();

            var command = (IExecutable)commandConstructor.Invoke(injectedParams);

            return command;
        }
    }
}