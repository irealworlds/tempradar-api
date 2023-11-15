using Newtonsoft.Json;

namespace API.Domain.Dto
{
    public class CurrentWeatherDto
    {
        [JsonProperty("location")]
        public LocationDto? Location { get; set; }

        [JsonProperty("current")]
        public CurrentDto? Current { get; set; }
    }
}
