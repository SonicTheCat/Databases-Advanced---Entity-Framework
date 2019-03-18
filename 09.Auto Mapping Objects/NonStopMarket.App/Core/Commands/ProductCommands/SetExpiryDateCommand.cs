namespace NonStopMarket.App.Core.Commands.ProductCommands
{
    using System;

    using NonStopMarket.App.Core.Contracts;
  
    public class SetExpiryDateCommand : IExecutable
    {
        public SetExpiryDateCommand(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public string Execute(string[] data)
        {
            var id = int.Parse(data[0]);
            var expDate = DateTime.Parse(data[1]);

            var product = this.UnitOfWork.Products.Get(id);
            product.ExpiryDate = expDate;

            this.UnitOfWork.Complete();

            return $"Expiry date for product with {id} has been changed!";
        }
    }
}