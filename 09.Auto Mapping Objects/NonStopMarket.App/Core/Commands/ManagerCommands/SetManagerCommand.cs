namespace NonStopMarket.App.Core.Commands.ManagerCommands
{
    using Contracts;

    public class SetManagerCommand : IExecutable
    {
        public SetManagerCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var employeeId = int.Parse(data[0]);
            var managerId = int.Parse(data[1]);

            this.UnitOfWork.Employees.SetManager(employeeId, managerId);
            this.UnitOfWork.Complete(); 

            return $"Employee with id {employeeId} has manager with id {managerId}!";
        }
    }
}