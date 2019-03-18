namespace NonStopMarket.App.Core.Dtos.ProductDtos
{
    using System;

    public class ExpiredDateDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int QuantityInstock { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}