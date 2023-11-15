using Newtonsoft.Json;

namespace API.Domain.DTO
{
    public class LocationDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("region")]
        public string? Region { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("tz_id")]
        public string? TzId { get; set; }

        [JsonProperty("localtime_epoch")]
        public int LocaltimeEpoch { get; set; }

        [JsonProperty("localtime")]
        public string? Localtime { get; set; }
    }
}
