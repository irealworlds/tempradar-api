using API.Domain.Contracts.Services;
using API.Domain.Dto;
using AutoMapper;

namespace API.Application.Services
{
    public class PinnedCityWeatherService(IPinnedCityService pinnedCityService, ICurrentWeatherService currentWeatherApiService, IWeatherHistoryService weatherHistoryService, IMapper mapper) : IPinnedCityWeatherService
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

            return mapper.Map<PinnedCityWeatherDetailsDto>(weather);
        }

        public async Task<List<CityDailyTemperatureDto>?> GetWeatherHistoryAsync(Guid id)
        {
            var city = await pinnedCityService.GetByIdAsync(id);

            if (city == null)
            {
                return null;
            }

            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now.AddDays(-1);
            var weatherHistory = await weatherHistoryService.GetWeatherHistoryForLocationAsync(city.Latitude, city.Longitude, startDate, endDate);

            if (weatherHistory.Forecast?.DailyForecasts == null)
            {
                return null;
            }

            var dailyTemperatures = new List<CityDailyTemperatureDto>();
            foreach (var day in weatherHistory.Forecast.DailyForecasts)
            {
                var dailyTemperature = new CityDailyTemperatureDto
                {
                    Date = day.Date,
                    MaximumTemperature = day.DailyConditions.MaximumTemperatureCelsius,
                    MinimumTemperature = day.DailyConditions.MinimumTemperatureCelsius,
                    AverageTemperature = day.DailyConditions.AverageTemperatureCelsius
                };
                dailyTemperatures.Add(dailyTemperature);
            }

            return dailyTemperatures;
        }
    }
}
