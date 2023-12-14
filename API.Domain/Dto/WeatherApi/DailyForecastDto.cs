using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi;

public class DailyForecastDto
{
    [JsonProperty("date")] public DateTime Date { get; set; }

    [JsonProperty("date_epoch")] public int DateEpoch { get; set; }

    [JsonProperty("day")] public DailyConditionsDto? DailyConditions { get; set; }

    [JsonProperty("astro")] public AstroDto? Astro { get; set; }

    [JsonProperty("hour")] public List<HourDto>? Hour { get; set; }
}