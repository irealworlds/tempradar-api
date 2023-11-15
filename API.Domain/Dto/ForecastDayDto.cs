using Newtonsoft.Json;

namespace API.Domain.Dto
{
    public class ForecastDayDto
    {
        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("date_epoch")]
        public int DateEpoch { get; set; }

        [JsonProperty("day")]
        public DayDto? Day { get; set; }

        [JsonProperty("astro")]
        public AstroDto? Astro { get; set; }

        [JsonProperty("hour")]
        public List<HourDto>? Hour { get; set; }
    }
}
