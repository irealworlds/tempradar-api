using API.Domain.Dto;

namespace API.Domain.Contracts.Services
{
    public interface ICurrentWeatherApiService
    {
        Task<CurrentWeatherDto> GetCurrentWeatherAsync(double lat, double lon);
    }
}
