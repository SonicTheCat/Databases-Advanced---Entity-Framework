namespace FastFood.App
{
    using AutoMapper;
    using FastFood.DataProcessor.Dto.Import;
    using FastFood.Models;

    public class FastFoodProfile : Profile
	{
		public FastFoodProfile()
		{
            //CreateMap<EmployeeDto, Employee>()
            //    .ForPath(dest => dest.Position.Name, opt => opt.MapFrom(src => src.Position));

            //CreateMap<ItemDto, Item>()
            //   .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.Category));
        }
	}
}