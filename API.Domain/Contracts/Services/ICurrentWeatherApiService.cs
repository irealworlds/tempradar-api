using API.Domain.Dto.WeatherApi;

namespace API.Domain.Contracts.Services;

public interface ICurrentWeatherApiService
{
    public Task<CurrentWeatherDto> GetCurrentWeatherForLocationAsync(double latitude, double longitude);
}