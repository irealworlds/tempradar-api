using System.Text.Json.Serialization;

namespace API.Domain.Dto.SensorApi;

public class SensorReadingDto
{
    [JsonPropertyName("resourceIdentifier")]
    public string ResourceIdentifier { get; set; } = string.Empty;

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }

    [JsonPropertyName("airQuality")]
    public double AirQuality { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}