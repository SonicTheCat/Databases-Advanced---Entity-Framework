namespace NonStopMarket.App.Core.Commands.EmployeeCommands
{
    using System;

    using Contracts;
    
    public class SetBirthdayCommand : IExecutable
    {
        public SetBirthdayCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public int MyProperty { get; set; }

        public string Execute(string[] data)
        {
            var id = int.Parse(data[0]);
            var birthday = DateTime.Parse(data[1]);

            this.UnitOfWork.Employees.SetBirthday(id, birthday);
            this.UnitOfWork.Complete();

            return $"Birthdate for user with id {id} has been changed!"; 
        }
    }
}