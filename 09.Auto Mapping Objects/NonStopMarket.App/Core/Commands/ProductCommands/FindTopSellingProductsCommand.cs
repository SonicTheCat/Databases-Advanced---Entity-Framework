namespace NonStopMarket.App.Core.Commands.ProductCommands
{
    using NonStopMarket.App.Core.Contracts;
    using System.Text;

    public class FindTopSellingProductsCommand : IExecutable
    {
        public FindTopSellingProductsCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var takeCount = int.Parse(data[0]);

            var products = this.UnitOfWork.Products.FindTopSellingProducts(takeCount);

            StringBuilder sb = new StringBuilder();

            var counter = 0;
            foreach (var product in products)
            {
                sb.AppendLine($"{++counter}. Name:{product.Name}, Price:{product.Price} SoldAmount:{product.SoldAmount}"); 
            }

            return sb.ToString().TrimEnd(); 
        }
    }
}