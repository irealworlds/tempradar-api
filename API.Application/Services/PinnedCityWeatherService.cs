using API.Domain.Contracts.Services;
using API.Domain.Dto;
using AutoMapper;

namespace API.Application.Services
{
    public class PinnedCityWeatherService(IPinnedCityService pinnedCityService, ICurrentWeatherApiService currentWeatherApiService, IMapper mapper) : IPinnedCityWeatherService
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
    }
}
