using Newtonsoft.Json;

namespace API.Domain.Dto
{
    public class ConditionDto
    {
        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("icon")]
        public string? Icon { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
