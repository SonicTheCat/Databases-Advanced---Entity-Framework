namespace ProductsShop
{
    using AutoMapper;
    using ProductsShop.Dtos;
    using ProductsShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>(); 
            CreateMap<ProductDto, Product>(); 
            CreateMap<CategoryDto, Category>(); 
        }
    }
}