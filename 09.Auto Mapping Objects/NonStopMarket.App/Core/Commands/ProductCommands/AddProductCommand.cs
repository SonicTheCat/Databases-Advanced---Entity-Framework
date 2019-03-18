namespace NonStopMarket.App.Core.Commands.ProductCommands
{
    using Contracts;
    using NonStopMarket.Models;

    public class AddProductCommand : IExecutable
    {
        public AddProductCommand(IUnitOfWork unitOfwork)
        {
            this.UnitOfWork = unitOfwork; 
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var name = data[0];
            var totalQ = int.Parse(data[1]);
            var qInStock = int.Parse(data[2]);
            var price = decimal.Parse(data[3]);

            var product = new Product()
            {
                Name = name,
                TotalQuantity = totalQ,
                QuantityInstock = qInStock,
                Price = price
            };

            this.UnitOfWork.Products.Add(product);
            this.UnitOfWork.Complete();

            return $"Product {name} was delivered in the market!"; 
        }
    }
}