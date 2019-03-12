namespace P01_BillsPaymentSystem.Core.Commands
{
    using P01_BillsPaymentSystem.Core.Commands.Contracts;

    public abstract class Command : IExecutable
    {
        public Command(string[] data)
        {
            this.Data = data;
        }

        public string[] Data { get; protected set; }

        public abstract void Execute();
    }
}