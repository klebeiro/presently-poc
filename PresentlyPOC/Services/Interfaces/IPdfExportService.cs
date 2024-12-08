namespace PresentlyPOC.Services.Interfaces
{
    public interface IPdfExportService
    {
        Task<byte[]> ExportAsync(string pdfContent);
    }
}
