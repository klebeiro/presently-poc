using PresentlyPOC.Models;

namespace PresentlyPOC.Infra.ExternalServices.Interfaces
{
    public interface IOpenAIService
    {
        Task<string> GenerateSlideContentAsync(SlideInformationRequest request);
    }
}
