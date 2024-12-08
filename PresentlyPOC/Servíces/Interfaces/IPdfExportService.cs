using Microsoft.AspNetCore.Mvc;

namespace PresentlyPOC.Servíces.Interfaces
{
    public interface IPdfExportService
    {
        Task<byte[]> ExportAsync(string pdfContent);
    }
}
