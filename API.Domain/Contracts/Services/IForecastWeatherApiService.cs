using API.Domain.DTO;

namespace API.Domain.Contracts.Services
{
    public interface IForecastWeatherApiService
    {
        Task<WeatherForecastDto> GetWeatherForecastAsync(double lat, double lon);
    }
}
