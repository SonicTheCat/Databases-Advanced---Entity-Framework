namespace NonStopMarket.App.Core
{
    using AutoMapper;

    using Dtos.EmployeeDtos;
    using Dtos.ManagerDtos;
    using Dtos.ProductDtos;
    using Dtos.OrderDtos;
    using NonStopMarket.Models;
    using System.Linq;

    public class NonStopMarketProfile : Profile
    {
        public NonStopMarketProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>().ReverseMap();

            CreateMap<Product, BestSellerProductDto>().ReverseMap();
            CreateMap<Product, ExpiredDateDto>().ReverseMap();
            CreateMap<Product, ProductPricesDto>().ReverseMap();

            CreateMap<Order, OrderIncomeDto>()
                .ForMember(dest => dest.Products,
                    opt => opt.MapFrom(src => src.ProductsOrders
                        .Select(x => x.Product))); 
        }
    }
}