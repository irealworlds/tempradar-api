using AutoMapper;
using API.Domain.Dto;
using API.Domain.Dto.WeatherApi;

namespace API.Infrastructure.Mapper;

public class WeatherApiMapperProfile : Profile
{
    public WeatherApiMapperProfile()
    {   
        CreateMap<PinnedCityWeatherDetailsDto, CurrentWeatherDto>()
            .ReverseMap()
            .ForMember(destination => destination.Temperature, options => options.MapFrom(source => source.Current!.TemperatureCelsius))
            .ForMember(destination => destination.FeelsLikeTemperature, options => options.MapFrom(source => source.Current.FeelsLikeCelsius))
            .ForMember(destination => destination.WindSpeed, options => options.MapFrom(source => source.Current.WindKph))
            .ForMember(destination => destination.AtmosphericPressure, options => options.MapFrom(source => source.Current.PressureMb))
            .ForMember(destination => destination.UltravioletIndex, options => options.MapFrom(source => source.Current.UltravioletIndex))
            .ForMember(destination => destination.CarbonMonoxide, options => options.MapFrom(source => source.Current.AirQuality!.CarbonMonoxide))
            .ForMember(destination => destination.NitrogenDioxide, options => options.MapFrom(source => source.Current.AirQuality.NitrogenDioxide))
            .ForMember(destination => destination.Ozone, options => options.MapFrom(source => source.Current.AirQuality.Ozone))
            .ForMember(destination => destination.SulphurDioxide, options =>options.MapFrom(source => source.Current.AirQuality.SulphurDioxide))
            .ForMember(destination => destination.Pm25, options => options.MapFrom(source => source.Current.AirQuality.Pm25))
            .ForMember(destination => destination.Pm10, options => options.MapFrom(source => source.Current.AirQuality.Pm10))
            .ForMember(destination => destination.UsEpaIndex, options => options.MapFrom(source => source.Current.AirQuality.UsEpaIndex))
            .ForMember(destination => destination.GbDefraIndex, options => options.MapFrom(source => source.Current.AirQuality.GbDefraIndex));
    }
    
}