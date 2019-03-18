namespace NonStopMarket.App.Core.Contracts
{
    public interface ICommandInterpreter
    {
        IExecutable InterpretCommand(string[] data); 
    }
}