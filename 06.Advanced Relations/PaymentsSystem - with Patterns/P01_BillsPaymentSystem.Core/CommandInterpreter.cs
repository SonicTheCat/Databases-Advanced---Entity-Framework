namespace P01_BillsPaymentSystem.Core
{
    using P01_BillsPaymentSystem.Core.Attributes;
    using P01_BillsPaymentSystem.Core.Commands.Contracts;
    using P01_BillsPaymentSystem.Core.Contracts;

    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string suffix = "Command";

        private readonly IServiceProvider serviceProvider; 

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IExecutable InterpretCommand(string[] data)
        {
            var commandType = data[0] + suffix;
            data = data.Skip(1).ToArray();

            Assembly assembly = Assembly.GetCallingAssembly();
            var model = assembly.GetTypes().FirstOrDefault(x => x.Name == commandType);

            if (model == null)
            {
                throw new ArgumentException("Invalid type!");
            }

            PropertyInfo[] propertiesToInject = model
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributes<InjectAttribute>().Any())
                .ToArray();

            var injectProps = propertiesToInject
                .Select(p => this.serviceProvider.GetService(p.PropertyType))
                .ToArray();

            var joinedParams = new object[] { data }.Concat(injectProps).ToArray();

            IExecutable command = (IExecutable)Activator.CreateInstance(model, joinedParams);

            return command; 
        }
    }
}