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
            .ForMember(destination => destination.Temperature, options => options.MapFrom(source => source.Current!.TempC))
            .ForMember(destination => destination.FeelsLikeTemperature, options => options.MapFrom(source => source.Current.FeelsLikeC))
            .ForMember(destination => destination.WindSpeed, options => options.MapFrom(source => source.Current.WindKph))
            .ForMember(destination => destination.AtmosphericPressure, options => options.MapFrom(source => source.Current.PressureMb))
            .ForMember(destination => destination.UltravioletIndex, options => options.MapFrom(source => source.Current.UV))
            .ForMember(destination => destination.CarbonMonoxide, options => options.MapFrom(source => source.Current.AirQuality!.CO))
            .ForMember(destination => destination.NitrogenDioxide, options => options.MapFrom(source => source.Current.AirQuality.NO2))
            .ForMember(destination => destination.Ozone, options => options.MapFrom(source => source.Current.AirQuality.O3))
            .ForMember(destination => destination.SulphurDioxide, options =>options.MapFrom(source => source.Current.AirQuality.SO2))
            .ForMember(destination => destination.Pm25, options => options.MapFrom(source => source.Current.AirQuality.PM2_5))
            .ForMember(destination => destination.Pm10, options => options.MapFrom(source => source.Current.AirQuality.PM10))
            .ForMember(destination => destination.UsEpaIndex, options => options.MapFrom(source => source.Current.AirQuality.USEPAIndex))
            .ForMember(destination => destination.GbDefraIndex, options => options.MapFrom(source => source.Current.AirQuality.GBDefraIndex));
    }
    
}