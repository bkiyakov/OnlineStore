using AutoMapper;
using OnlineStore.Application.Dtos;
using OnlineStore.Domain.Models;

namespace OnlineStore.Application.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<Order, OrderDto>()
                .ReverseMap();
            CreateMap<OrderElement, OrderElementDto>()
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.ItemPrice * src.ItemsCount))
                .ReverseMap();
        }
    }
}
