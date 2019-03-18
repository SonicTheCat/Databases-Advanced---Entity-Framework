namespace NonStopMarket.App.Core.Commands.OrderCommands
{
    using System;

    using Contracts;
    using NonStopMarket.Models;
    using NonStopMarket.Models.Enums;
    
    public class AddOrderCommand : IExecutable
    {
        public AddOrderCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var empId = int.Parse(data[0]);
            var commandToEnum = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), data[1], true);

            var order = new Order()
            {
                PaymentMethod = commandToEnum,
                Date = DateTime.Now,
                EmployeeId = empId
            };

            this.UnitOfWork.Orders.Add(order);
            this.UnitOfWork.Complete();

            return "Order was added!"; 
        }
    }
}