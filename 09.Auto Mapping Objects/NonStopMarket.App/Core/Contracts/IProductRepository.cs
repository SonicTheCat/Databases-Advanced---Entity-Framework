namespace NonStopMarket.App.Core.Contracts
{
    using System.Collections.Generic;

    using NonStopMarket.Models;
    using Dtos.ProductDtos;

    public interface IProductRepository : IRepository<Product>
    {
        BestSellerProductDto FindBestSeller(); 

        IEnumerable<Product> FindTopSellingProducts(int productsCount);

        IEnumerable<ExpiredDateDto> GetAllWithExpiredDate();

        IEnumerable<ProductPricesDto> GetMostExpesiveProducts(int productsCount); 

        IEnumerable<ProductPricesDto> GetCheapestProducts(int productsCount); 
    }
}