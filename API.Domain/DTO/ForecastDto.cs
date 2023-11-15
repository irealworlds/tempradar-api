using Newtonsoft.Json;

namespace API.Domain.DTO
{
    public class ForecastDto
    {
        [JsonProperty("forecastday")]
        public List<ForecastDayDto>? Forecastday { get; set; }
    }
}
