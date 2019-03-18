namespace NonStopMarket.App.Core.Dtos.OrderDtos
{
    using System.Collections.Generic;
    using System.Linq;

    using ProductDtos;
    
    public class OrderIncomeDto
    {
        public int OrderId { get; set; }

        public decimal Income => this.Products.Sum(p => p.Price); 

        public ICollection<ProductPricesDto> Products { get; set; } = new List<ProductPricesDto>();
    }
}