namespace NonStopMarket.App.Core.Commands.EmployeeCommands
{
    using NonStopMarket.App.Core.Contracts;

    public class EmployeeInfoCommand : IExecutable
    {
        public EmployeeInfoCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var id = int.Parse(data[0]);

            var employee = this.UnitOfWork.Employees.EmployeeInfo(id);

            return employee.ToString();
        }
    }
}