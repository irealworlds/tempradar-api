using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Dto.WeatherApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Infrastructure.WeatherApi.Services
{
    public class HistoryWeatherApiService(HttpClient client, IOptions<WeatherApiSettings> options) :
        AbstractWeatherApiService(options),
        IWeatherHistoryService
    {
        public async Task<WeatherHistoryDto> GetWeatherHistoryForLocationAsync(double latitude, double longitude, DateTime date)
        {
            var requestUri = BuildEndpointUri("/history.json", new Dictionary<string, string>
            {
                { "q", $"{latitude},{longitude}" },
                { "dt", date.ToString("yyyy-MM-dd") }
            });
            var response = await client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Failed to retrieve weather history data. Status code: {response.StatusCode}"
                );
            }

            var json = await response.Content.ReadAsStringAsync();
            var weatherHistoryDto = JsonConvert.DeserializeObject<WeatherHistoryDto>(json);

            if (weatherHistoryDto == null)
            {
                throw new Exception("Could not parse data returned from WeatherApi.");
            }

            return weatherHistoryDto;
        }

        public async Task<WeatherHistoryDto> GetWeatherHistoryForLocationAsync(double latitude, double longitude, DateTime startDate, DateTime endDate)
        {
            var requestUri = BuildEndpointUri("/history.json", new Dictionary<string, string>
            {
                { "q", $"{latitude},{longitude}" },
                { "dt", startDate.ToString("yyyy-MM-dd") },
                { "end_dt", endDate.ToString("yyyy-MM-dd") }
            });
            var response = await client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Failed to retrieve weather history data. Status code: {response.StatusCode}"
                );
            }

            var json = await response.Content.ReadAsStringAsync();
            var weatherHistoryDto = JsonConvert.DeserializeObject<WeatherHistoryDto>(json);

            if (weatherHistoryDto == null)
            {
                throw new Exception("Could not parse data returned from WeatherApi.");
            }

            return weatherHistoryDto;
        }
    }
}
