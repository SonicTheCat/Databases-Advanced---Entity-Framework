namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using FastFood.Models;
    using AutoMapper.QueryableExtensions;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Name).ToList(),
                Employees = this.context.Employees.Select(x => x.Name).ToList(),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home"); 
            }

            var order = this.mapper.Map<Order>(model);

            var employeeId = this.context
                .Employees
                .Where(e => e.Name == model.EmployeeName)
                .FirstOrDefault()
                .Id;

            var itemId = this.context
                .Items
                .Where(i => i.Name == model.ItemName)
                .FirstOrDefault()
                .Id;

            order.EmployeeId = employeeId;

            order.OrderItems.Add(new OrderItem()
            {
                Order = order,
                ItemId = itemId, 
                Quantity = model.Quantity
            });

            this.context.Orders.Add(order);

            this.context.SaveChanges(); 

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context.Orders
                .ProjectTo<OrderAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders); 
        }
    }
}