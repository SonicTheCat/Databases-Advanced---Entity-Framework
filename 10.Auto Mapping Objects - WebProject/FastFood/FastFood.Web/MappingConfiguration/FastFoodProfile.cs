namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using Models;

    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Employees
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(p => p.Name));

            this.CreateMap<RegisterEmployeeInputModel, Employee>()
                .ReverseMap();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(e => e.Position.Name));

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            //Items
            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            //Orders
            this.CreateMap<CreateOrderInputModel, Order>();

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToShortDateString()))
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee.Name)); 
        }
    }
}