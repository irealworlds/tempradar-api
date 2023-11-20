using API.Domain.Contracts.Services;
using API.Domain.Dto;

namespace API.Application.Services
{
    public class PinnedCityWeatherService(IPinnedCityService pinnedCityService, ICurrentWeatherApiService currentWeatherApiService) : IPinnedCityWeatherService
    {
        public async Task<PinnedCityWeatherDetailsDto> GetWeatherDetailsAsync(Guid id)
        {
            var city = await pinnedCityService.GetByIdAsync(id);

            if (city == null)
            {
                return null;
            }

            var weather = await currentWeatherApiService.GetCurrentWeatherAsync(city.Latitude, city.Longitude);

            if (weather == null)
            {
                return null;
            }

            var details = new PinnedCityWeatherDetailsDto
            {
                TempC = weather.Current.TempC,
                FeelsLikeC = weather.Current.FeelsLikeC,
                WindKph = weather.Current.WindKph,
                PressureMb = weather.Current.PressureMb,
                UV = weather.Current.UV,
                CO = weather.Current.AirQuality.CO,
                NO2 = weather.Current.AirQuality.NO2,
                O3 = weather.Current.AirQuality.O3,
                SO2 = weather.Current.AirQuality.SO2,
                PM2_5 = weather.Current.AirQuality.PM2_5,
                PM10 = weather.Current.AirQuality.PM10,
                USEPAIndex = weather.Current.AirQuality.USEPAIndex,
                GBDefraIndex = weather.Current.AirQuality.GBDefraIndex
            };

            return details;
        }
    }
}
