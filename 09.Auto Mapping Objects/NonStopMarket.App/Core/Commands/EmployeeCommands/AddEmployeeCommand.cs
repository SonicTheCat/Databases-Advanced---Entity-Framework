namespace NonStopMarket.App.Core.Commands.EmployeeCommands
{
    using Contracts;
    using Dtos.EmployeeDtos;

    public class AddEmployeeCommand : IExecutable
    {
        public AddEmployeeCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork; 
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            EmployeeDto employee = new EmployeeDto()
            {
                FirstName = data[0],
                LastName = data[1],
                Salary = decimal.Parse(data[2])
            };

            this.UnitOfWork.Employees.AddEmployee(employee);
            this.UnitOfWork.Complete();

            return $"User with name {data[0]} {data[1]} has been added!"; 
        }
    }
}