using API.Domain.Dto.WeatherApi;

namespace API.Domain.Contracts.Services
{
    public interface IWeatherHistoryService
    {
        Task<WeatherHistoryDto> GetWeatherHistoryForLocationAsync(double latitude, double longitude, DateTime date);
        Task<WeatherHistoryDto> GetWeatherHistoryForLocationAsync(double latitude, double longitude, DateTime startDate, DateTime endDate);
    }
}
