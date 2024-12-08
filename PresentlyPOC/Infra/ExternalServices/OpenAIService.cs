using PresentlyPOC.Infra.ExternalServices.Interfaces;
using PresentlyPOC.Models;
using System.Text.Json;
using System.Text;
using PresentlyPOC.Models.OpenAI;
using System.Reflection.Metadata;

namespace PresentlyPOC.Infra.ExternalServices
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenAIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GenerateSlideContentAsync(SlideInformationRequest request)
        {
            var apiKey = request.ApiKey;

            if (string.IsNullOrWhiteSpace(request.ApiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            var httpClient = _httpClientFactory.CreateClient("OpenAIApi");

            var payload = new
            {
                model = "gpt-4o-mini",
                messages = new List<OpenAIMessageRequestDto>{
                    new OpenAIMessageRequestDto
                    {
                        Role = "user",
                        Content = $"Crie um slide em LaTeX sobre o tema {request.Theme} com a autoria de {request.Author}. Observações: {request.Comments}. Forneça apenas o código LaTeX, incluindo o \\begin{{document}} e \\end{{document}}, sem explicações ou formatação extra. Se não for possível gerar o slide, retorne apenas uma resposta vazia."
                    }
                },
                max_tokens = 1000,
                temperature = 0.7
            };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var response = await httpClient.PostAsync("v1/chat/completions", content);

            var resjson = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonSerializer.Deserialize<OpenAIErrorResponseDto>(resjson);
                throw new System.Exception(errorResponse?.Error.Message ?? "Something went wrong with the OpenAPI request");
            }

            var data = JsonSerializer.Deserialize<OpenAIResponseDto>(resjson);
            return data?.Choices[0].Message.Content ?? "";

        }
    }
}
