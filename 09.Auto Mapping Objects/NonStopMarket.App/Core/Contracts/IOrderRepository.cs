namespace NonStopMarket.App.Core.Contracts
{
    using System.Collections.Generic;

    using Dtos.OrderDtos;
    using NonStopMarket.Models;

    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<OrderIncomeDto> GetOrdersByIncome(); 
    }
}