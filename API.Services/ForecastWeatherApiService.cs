using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Services
{
    public class ForecastWeatherApiService : IForecastWeatherApiService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "http://api.weatherapi.com/v1/forecast.json";

        public ForecastWeatherApiService(HttpClient client, IOptions<WeatherApiSettings> options)
        {
            _client = client;
            baseUrl += $"?key={options.Value.ApiKey}";
        }

        public async Task<WeatherForecastDto> GetWeatherForecastAsync(double lat, double lon)
        {
            var requestUrl = $"{baseUrl}&q={lat},{lon}&days=3&aqi=yes&alerts=no";
            var response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherForecastDto = JsonConvert.DeserializeObject<WeatherForecastDto>(json);
                return weatherForecastDto;
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
