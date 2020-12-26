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
        }
    }
}
