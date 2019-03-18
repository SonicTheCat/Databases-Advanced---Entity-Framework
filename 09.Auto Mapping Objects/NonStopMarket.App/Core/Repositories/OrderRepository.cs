namespace NonStopMarket.App.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Dtos.OrderDtos;
    using NonStopMarket.Data;
    using NonStopMarket.Models;

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public NonStopMarketContext NonStopMarketContext => this.context as NonStopMarketContext;

        public IEnumerable<OrderIncomeDto> GetOrdersByIncome()
        {
            var orders = this.NonStopMarketContext.Orders
                  .Include(x => x.ProductsOrders)
                  .ThenInclude(x => x.Product)
                  .ProjectTo<OrderIncomeDto>()
                  .OrderBy(x => x.Income)
                  .ToList();

            return orders; 
        }
    }
}