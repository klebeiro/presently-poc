using PresentlyPOC.Models;
using PresentlyPOC.Services.Interfaces;
using PresentlyPOC.Services;
using PresentlyPOC.Infra.ExternalServices.Interfaces;
using PresentlyPOC.Infra.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPdfExportService, PdfExportService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000") 
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient("OpenAIApi", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/");
    client.Timeout = TimeSpan.FromSeconds(60);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/generate-pdf", async (HttpContext context, IPdfExportService pdfService, IOpenAIService openAIService, SlideInformationRequest slideInformation) =>
{
    try {
        var content = await openAIService.GenerateSlideContentAsync(slideInformation);
        var pdfResult = await pdfService.ExportAsync(content);
        return Results.File(pdfResult, "application/pdf", "slide.pdf");
    }
    catch(Exception exception)
    {
        return Results.Problem(exception.Message, statusCode: 500);
    }
});

app.Run();