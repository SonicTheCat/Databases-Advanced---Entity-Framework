namespace P01_BillsPaymentSystem.Core
{
    using System;

    using Contracts;
    using P01_BillsPaymentSystem.Core.IO.Contracts;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            this.writer.WriteLine(OutputMessages.WelcomeString());

            while (true)
            {
                try
                {
                    this.writer.WriteLine(OutputMessages.MenuOptions());

                    var result = string.Empty;
                    var input = this.reader.ReadLine().Split();

                    var command = this.commandInterpreter.InterpretCommand(input);
                    command.Execute(); 
                }
                catch (Exception)
                {
                    this.writer.WriteLine(OutputMessages.InvalidCommand); 
                }
            }
        }
    }
}