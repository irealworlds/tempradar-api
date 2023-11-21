using API.Domain.Dto.WeatherApi;

namespace API.Domain.Contracts.Services;

public interface IWeatherForecastService
{
    public Task<WeatherForecastDto> GetWeatherForecastForLocationAsync(double latitude, double longitude);
}