using Microsoft.AspNetCore.Mvc;
using PresentlyPOC.Servíces.Interfaces;
using System.Diagnostics;

namespace PresentlyPOC.Services
{
    public class PdfExportService : IPdfExportService
    {
        public async Task<byte[]> ExportAsync(string pdfContent)
        {
            if (string.IsNullOrEmpty(pdfContent)) { 
                throw new ArgumentNullException(nameof(pdfContent));
            }

            string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);

            string texFilePath = Path.Combine(tempFolder, "slide.tex");
            string pdfFilePath = Path.Combine(tempFolder, "slide.pdf");

            try { 
                await File.WriteAllTextAsync(texFilePath, pdfContent);

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "pdflatex",
                    Arguments = $"\"{texFilePath}\"",
                    WorkingDirectory = tempFolder,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                var process = new Process { StartInfo = processStartInfo };

                process.Start();

                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                await Task.WhenAll(outputTask, errorTask);

                process.WaitForExit();

                if (!File.Exists(pdfFilePath))
                {
                    Directory.Delete(tempFolder, true);
                    throw new InvalidOperationException("Erro ao gerar o slide.");
                }

                var pdfBytes = await File.ReadAllBytesAsync(pdfFilePath);
                Directory.Delete(tempFolder, true);

                return pdfBytes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao gerar o PDF: " + ex.Message, ex);
            }
            finally
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
        }
    }
}
