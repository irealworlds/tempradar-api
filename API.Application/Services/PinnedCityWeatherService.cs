using API.Domain.Contracts.Services;
using API.Domain.Dto;

namespace API.Application.Services
{
    public class PinnedCityWeatherService(IPinnedCityService pinnedCityService, ICurrentWeatherApiService currentWeatherApiService) : IPinnedCityWeatherService
    {
        public async Task<PinnedCityWeatherDetailsDto?> GetWeatherDetailsAsync(Guid id)
        {
            var city = await pinnedCityService.GetByIdAsync(id);

            if (city == null)
            {
                return null;
            }

            var weather = await currentWeatherApiService.GetCurrentWeatherForLocationAsync(city.Latitude, city.Longitude);

            if (weather == null)
            {
                return null;
            }

            var details = new PinnedCityWeatherDetailsDto
            {
                Temperature = weather.Current!.TempC,
                FeelsLikeTemperature = weather.Current.FeelsLikeC,
                WindSpeed = weather.Current.WindKph,
                AtmosphericPressure = weather.Current.PressureMb,
                UltravioletIndex = weather.Current.UV,
                CarbonMonoxide = weather.Current.AirQuality!.CO,
                NitrogenDioxide = weather.Current.AirQuality.NO2,
                Ozone = weather.Current.AirQuality.O3,
                SulphurDioxide = weather.Current.AirQuality.SO2,
                Pm25 = weather.Current.AirQuality.PM2_5,
                Pm10 = weather.Current.AirQuality.PM10,
                UsEpaIndex = weather.Current.AirQuality.USEPAIndex,
                GbDefraIndex = weather.Current.AirQuality.GBDefraIndex
            };

            return details;
        }
    }
}
