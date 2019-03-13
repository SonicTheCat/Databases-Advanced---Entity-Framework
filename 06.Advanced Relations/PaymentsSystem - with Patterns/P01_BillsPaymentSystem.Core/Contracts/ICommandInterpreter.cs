namespace P01_BillsPaymentSystem.Core.Contracts
{
    using P01_BillsPaymentSystem.Core.Commands.Contracts;

    public interface ICommandInterpreter
    {
        IExecutable InterpretCommand(string[] data); 
    }
}