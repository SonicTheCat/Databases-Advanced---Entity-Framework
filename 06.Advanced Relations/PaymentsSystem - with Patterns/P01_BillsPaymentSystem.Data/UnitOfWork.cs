namespace P01_BillsPaymentSystem.Data
{
    using Repositories;
    using Repositories.Contracts;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BillsPaymentSystemContext context;

        public UnitOfWork(BillsPaymentSystemContext context)
        {
            this.context = context;
            this.Users = new UserRepository(this.context);
        }

        public IUserRepository Users { get; private set; }

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