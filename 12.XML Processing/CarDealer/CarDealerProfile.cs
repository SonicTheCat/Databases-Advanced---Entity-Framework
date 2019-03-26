namespace CarDealer
{
    using AutoMapper;
    using CarDealer.Dtos;
    using CarDealer.Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDto, Supplier>();
            CreateMap<ImportCustomersDto, Customer>();
            CreateMap<ImportCarDto, Car>();
            CreateMap<ImportPartDto, Part>();
        }
    }
}