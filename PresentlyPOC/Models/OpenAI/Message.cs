using System.Text.Json.Serialization;

namespace PresentlyPOC.Models.OpenAI
{
    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
