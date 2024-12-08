using System.Text.Json.Serialization;

namespace PresentlyPOC.Models.OpenAI
{
    public class Choice
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("logprobs")]
        public object LogProblems { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }
}
