using API.Domain.DTO;

namespace API.Domain.Contracts.Services
{
    public interface ICurrentWeatherApiService
    {
        Task<CurrentWeatherDto> GetCurrentWeatherAsync(double lat, double lon);
    }
}
