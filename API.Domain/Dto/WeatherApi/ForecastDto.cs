using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi;

public class ForecastDto
{
    [JsonProperty("forecastday")] public IEnumerable<DailyForecastDto>? DailyForecasts { get; set; }
}