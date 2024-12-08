using System.Text.Json.Serialization;

namespace PresentlyPOC.Models.OpenAI
{
    public class OpenAIMessageRequestDto
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
