namespace NonStopMarket.App.Core.Commands.EmployeeCommands
{
    using Contracts;

    public class EmployeePersonalInfoCommand : IExecutable
    {
        public EmployeePersonalInfoCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var id = int.Parse(data[0]);

            var employee = this.UnitOfWork.Employees.EmployeePersonalInfo(id);

            return employee.ToString(); 
        }
    }
}