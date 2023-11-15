using API.Domain.Dto;

namespace API.Domain.Contracts.Services
{
    public interface IForecastWeatherApiService
    {
        Task<WeatherForecastDto> GetWeatherForecastAsync(double lat, double lon);
    }
}
