using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Services
{
    public class CurrentWeatherApiService : ICurrentWeatherApiService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "http://api.weatherapi.com/v1/current.json";

        public CurrentWeatherApiService(HttpClient client, IOptions<WeatherApiSettings> options)
        {
            _client = client;
            baseUrl += $"?key={options.Value.ApiKey}";
        }

        public async Task<CurrentWeatherDto> GetCurrentWeatherAsync(double lat, double lon)
        {
            var requestUrl = $"{baseUrl}&q={lat},{lon}&aqi=yes";
            var response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var currentWeatherDto = JsonConvert.DeserializeObject<CurrentWeatherDto>(json);
                return currentWeatherDto;
            }
            else
            {
                throw new HttpRequestException(
                    $"Failed to retrieve weather forecast data. Status code: {response.StatusCode}"
                );
            }
        }
    }
}
