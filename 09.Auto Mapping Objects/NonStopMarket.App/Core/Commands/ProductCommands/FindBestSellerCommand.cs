namespace NonStopMarket.App.Core.Commands.ProductCommands
{
    using NonStopMarket.App.Core.Contracts;

    public class FindBestSellerCommand : IExecutable
    {
        public FindBestSellerCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var bestSeller = this.UnitOfWork.Products.FindBestSeller();

            return $"ID: {bestSeller.ProductId}, Name: {bestSeller.Name}, SoldAmount: {bestSeller.SoldAmount}";
        }
    }
}