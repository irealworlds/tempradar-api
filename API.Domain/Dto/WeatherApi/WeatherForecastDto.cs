using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi
{
    public class WeatherForecastDto
    {
        [JsonProperty("location")]
        public LocationDto? Location { get; set; }

        [JsonProperty("current")]
        public CurrentConditionsDto? CurrentConditions { get; set; }

        [JsonProperty("forecast")]
        public ForecastDto? Forecast { get; set; }
    }
}
