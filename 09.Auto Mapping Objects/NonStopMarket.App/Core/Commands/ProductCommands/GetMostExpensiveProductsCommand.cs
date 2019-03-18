namespace NonStopMarket.App.Core.Commands.ProductCommands
{
    using System.Text;

    using NonStopMarket.App.Core.Contracts;
    
    public class GetMostExpensiveProductsCommand : IExecutable
    {
        public GetMostExpensiveProductsCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var takeCounter = int.Parse(data[0]);

            var products = this.UnitOfWork.Products.GetMostExpesiveProducts(takeCounter);

            StringBuilder sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"ID:{product.ProductId}, Name:{product.Name}, Price:{product.Price}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
