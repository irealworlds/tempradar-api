using Newtonsoft.Json;

namespace API.Domain.Dto
{
    public class WeatherForecastDto
    {
        [JsonProperty("location")]
        public LocationDto? Location { get; set; }

        [JsonProperty("current")]
        public CurrentDto? Current { get; set; }

        [JsonProperty("forecast")]
        public ForecastDto? Forecast { get; set; }
    }
}
