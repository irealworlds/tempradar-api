using API.Domain.Dto;
using API.Domain.Entities;
using AutoMapper;

namespace API.Infrastructure.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<IdentityDto, ApplicationUser>()
            .ReverseMap();
        CreateMap<PinnedCityDto, PinnedCity>()
            .ReverseMap();
    }
}