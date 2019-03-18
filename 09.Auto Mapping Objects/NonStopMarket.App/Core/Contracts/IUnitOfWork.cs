namespace NonStopMarket.App.Core.Contracts
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        IProductRepository Products { get; }

        IOrderRepository Orders { get; }

        int Complete();
    }
}