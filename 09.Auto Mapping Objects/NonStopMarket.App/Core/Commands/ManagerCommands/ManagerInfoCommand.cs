namespace NonStopMarket.App.Core.Commands.ManagerCommands
{
    using Contracts;

    class ManagerInfoCommand : IExecutable
    {
        public ManagerInfoCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var id = int.Parse(data[0]);

            var manager = this.UnitOfWork.Employees.ManagerInfo(id);

            return manager.ToString();
        }
    }
}
