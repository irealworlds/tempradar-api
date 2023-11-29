using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi;

public class AirQualityDto
{
    [JsonProperty("co")] public double CarbonMonoxide { get; set; }

    [JsonProperty("no2")] public double NitrogenDioxide { get; set; }

    [JsonProperty("o3")] public double Ozone { get; set; }

    [JsonProperty("so2")] public double SulphurDioxide { get; set; }

    [JsonProperty("pm2_5")] public double Pm25 { get; set; }

    [JsonProperty("pm10")] public double Pm10 { get; set; }

    [JsonProperty("us-epa-index")] public int UsEpaIndex { get; set; }

    [JsonProperty("gb-defra-index")] public int GbDefraIndex { get; set; }
}