using Newtonsoft.Json;

namespace API.Domain.Dto
{
    public class ForecastDto
    {
        [JsonProperty("forecastday")]
        public List<ForecastDayDto>? Forecastday { get; set; }
    }
}
