using Newtonsoft.Json;

namespace API.Domain.DTO
{
    public class CurrentWeatherDto
    {
        [JsonProperty("location")]
        public LocationDto? Location { get; set; }

        [JsonProperty("current")]
        public CurrentDto? Current { get; set; }
    }
}
