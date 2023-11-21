using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi
{
    public class AirQualityDto
    {
        [JsonProperty("co")]
        public double CO { get; set; }

        [JsonProperty("no2")]
        public double NO2 { get; set; }

        [JsonProperty("o3")]
        public double O3 { get; set; }

        [JsonProperty("so2")]
        public double SO2 { get; set; }

        [JsonProperty("pm2_5")]
        public double PM2_5 { get; set; }

        [JsonProperty("pm10")]
        public double PM10 { get; set; }

        [JsonProperty("us-epa-index")]
        public int USEPAIndex { get; set; }

        [JsonProperty("gb-defra-index")]
        public int GBDefraIndex { get; set; }
    }
}
