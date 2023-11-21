using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Dto.WeatherApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Infrastructure.WeatherApi.Services;

public class CurrentWeatherApiService(HttpClient client, IOptions<WeatherApiSettings> options) :
    AbstractWeatherApiService(options),
    ICurrentWeatherApiService
{
    public async Task<CurrentWeatherDto> GetCurrentWeatherForLocationAsync(double latitude, double longitude)
    {
        var requestUri = BuildEndpointUri("/current.json", new Dictionary<string, string>
        {
            { "q", $"{latitude},{longitude}" },
            { "aqi", "yes" }
        });
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Failed to retrieve weather forecast data. Status code: {response.StatusCode}"
            );
        }

        var json = await response.Content.ReadAsStringAsync();
        var currentWeatherDto = JsonConvert.DeserializeObject<CurrentWeatherDto>(json);

        if (currentWeatherDto == null)
        {
            throw new Exception("Could not parse data returned from WeatherApi.");
        }

        return currentWeatherDto;
    }
}