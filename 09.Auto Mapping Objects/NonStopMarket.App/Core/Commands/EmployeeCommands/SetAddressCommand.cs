namespace NonStopMarket.App.Core.Commands.EmployeeCommands
{
    using NonStopMarket.App.Core.Contracts;
    using System.Linq;

    public class SetAddressCommand : IExecutable
    {
        public SetAddressCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var id = int.Parse(data[0]);
            var address = string.Join(" ", data.Skip(1));

            this.UnitOfWork.Employees.SetAddress(id, address);
            this.UnitOfWork.Complete();

            return $"Address for user with id {id} has been changed!";
        }
    }
}