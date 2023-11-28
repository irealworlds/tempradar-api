using System.Text.Json.Serialization;

namespace API.Domain.Dto
{
    public class SensorDto
    {
        [JsonPropertyName("resourceIdentifier")]
        public string ResourceIdentifier { get; set; } = String.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;
        [JsonPropertyName("macAddress")]
        public string MacAddress { get; set; } = String.Empty;
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
