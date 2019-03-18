namespace NonStopMarket.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using NonStopMarket.Data;

    public class DbInitializer : IDbInitializer
    {
        private readonly NonStopMarketContext context; 

        public DbInitializer(NonStopMarketContext context)
        {
            this.context = context; 
        }

        public void InitializeDatabase()
        {
            context.Database.Migrate(); 
        }
    }
}