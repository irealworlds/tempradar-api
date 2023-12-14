using System.Text.Json.Serialization;

namespace API.Domain.Dto.SensorApi;

public class SensorDto
{
    [JsonPropertyName("resourceIdentifier")]
    public string ResourceIdentifier { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("macAddress")]
    public string MacAddress { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}