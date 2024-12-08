using System.Text.Json.Serialization;

namespace PresentlyPOC.Models.OpenAI
{
    public class OpenAIErrorResponseDto
    {
        [JsonPropertyName("error")]
        public OpenAIError Error { get; set; }
    }
}
