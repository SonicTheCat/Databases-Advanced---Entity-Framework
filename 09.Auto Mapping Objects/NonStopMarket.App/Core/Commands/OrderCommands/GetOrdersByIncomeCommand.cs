namespace NonStopMarket.App.Core.Commands.OrderCommands
{
    using System.Text;

    using NonStopMarket.App.Core.Contracts;

   public class GetOrdersByIncomeCommand : IExecutable
    {
        public GetOrdersByIncomeCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var orders = this.UnitOfWork.Orders.GetOrdersByIncome();

            StringBuilder sb = new StringBuilder();

            foreach (var order in orders)
            {
                sb.AppendLine($"ID: {order.OrderId}, Income: ${order.Income}");

                foreach (var product in order.Products)
                {
                    sb.AppendLine($"--- ProductName: {product.Name} Price: ${product.Price}"); 
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}