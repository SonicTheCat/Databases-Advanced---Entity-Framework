namespace NonStopMarket.App.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Dtos.ProductDtos;
    using NonStopMarket.Data;
    using NonStopMarket.Models;
    using AutoMapper.QueryableExtensions;

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public NonStopMarketContext NonStopMarketContext => this.context as NonStopMarketContext;

        public BestSellerProductDto FindBestSeller()
        {
            var product = this.NonStopMarketContext
                .Products
                .OrderByDescending(x => x.SoldAmount)
                .ToArray()[0];

            var dto = this.mapper.Map<BestSellerProductDto>(product);

            return dto;
        }

        public IEnumerable<Product> FindTopSellingProducts(int productsCount)
        {
            return this.NonStopMarketContext
               .Products
               .OrderByDescending(x => x.SoldAmount)
               .Take(productsCount)
               .ToArray();
        }

        public IEnumerable<ExpiredDateDto> GetAllWithExpiredDate()
        {
            return this.NonStopMarketContext
                .Products
                .Where(x => x.ExpiryDate.Value.Year < DateTime.Now.Year)
                .ProjectTo<ExpiredDateDto>()
                .ToList();
        }

        public IEnumerable<ProductPricesDto> GetCheapestProducts(int productsCount)
        {
            return this.NonStopMarketContext.Products
                .OrderBy(p => p.Price)
                .Take(productsCount)
                .ProjectTo<ProductPricesDto>()
                .ToArray();
        }

        public IEnumerable<ProductPricesDto> GetMostExpesiveProducts(int productsCount)
        {
            return this.NonStopMarketContext.Products
               .OrderByDescending(p => p.Price)
               .Take(productsCount)
               .ProjectTo<ProductPricesDto>()
               .ToArray();
        }
    }
}