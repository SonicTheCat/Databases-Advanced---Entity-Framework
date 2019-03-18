namespace NonStopMarket.App.Core.Commands.ProductCommands
{
    using System.Text;

    using Contracts;

    public class GetAllWithExpiredDateCommand : IExecutable
    {

        public GetAllWithExpiredDateCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var products = this.UnitOfWork.Products.GetAllWithExpiredDate();

            StringBuilder sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"ID:{product.ProductId}, Name:{product.Name}, Qiantity Instock:{product.QuantityInstock} Expired:{product.ExpiryDate.ToString("dd-mm-yyyy")}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}