﻿using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Dto.WeatherApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Infrastructure.WeatherApi.Services;

public class ForecastWeatherApiService(HttpClient client, IOptions<WeatherApiSettings> options) :
    AbstractWeatherApiService(options),
    IWeatherForecastService
{
    public async Task<WeatherForecastDto> GetWeatherForecastForLocationAsync(double latitude, double longitude)
    {
        var requestUri = this.BuildEndpointUri("/forecast.json", new Dictionary<string, string>
        {
            { "q", $"{latitude},{longitude}" },
            { "days", "3" },
            { "alerts", "no" }
        });
        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Failed to retrieve weather forecast data. Status code: {response.StatusCode}"
            );

        var json = await response.Content.ReadAsStringAsync();
        var weatherForecastDto = JsonConvert.DeserializeObject<WeatherForecastDto>(json);

        if (weatherForecastDto == null) throw new Exception("Could not parse data returned from WeatherApi.");

        return weatherForecastDto;
    }
}