using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi;

public class CurrentWeatherDto
{
    [JsonProperty("location")] public LocationDto? Location { get; set; }

    [JsonProperty("current")] public CurrentConditionsDto? Current { get; set; }
}