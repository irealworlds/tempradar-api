using API.Domain.Dto;
using API.Domain.Entities;
using AutoMapper;

namespace API.Infrastructure.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        this.CreateMap<IdentityDto, ApplicationUser>()
            .ReverseMap();
        this.CreateMap<PinnedCityDto, PinnedCity>()
            .ReverseMap();
        this.CreateMap<PinnedSensorDto, PinnedSensor>()
            .ReverseMap();
    }
}