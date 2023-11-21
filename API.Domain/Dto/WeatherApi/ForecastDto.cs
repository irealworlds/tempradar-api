using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi
{
    public class ForecastDto
    {
        [JsonProperty("forecastday")]
        public List<ForecastDayDto>? Forecastday { get; set; }
    }
}
