namespace NonStopMarket.App.Core
{
    using AutoMapper;

    using Contracts;
    using Repositories;
    using NonStopMarket.Data;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly NonStopMarketContext context;

        public UnitOfWork(NonStopMarketContext context, IMapper mapper)
        {
            this.context = context;
            this.Employees = new EmployeeRepository(context, mapper);
            this.Products = new ProductRepository(context, mapper);
            this.Orders = new OrderRepository(context, mapper);
        }

        public IEmployeeRepository Employees { get; private set; }

        public IProductRepository Products { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}