using Microsoft.Extensions.FileProviders;
using OllamaSharp;
using Uncencored_model_app.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var uri = new Uri("http://localhost:11434");
var ollama = new OllamaApiClient(uri);
const string MODEL_NAME = "uncencored-model-v1";
ollama.SelectedModel = MODEL_NAME;

// DI
builder.Services.AddSingleton(ollama);
builder.Services.AddSingleton<ChatService>();

var app = builder.Build();

app.UseCors("AllowAll");
// Serve static files from wwwroot
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "",
    EnableDefaultFiles = true // This enables serving default files like index.html
});
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers(); // This maps the endpoints for your controllers
    _ = endpoints.MapGet("/", async static context =>
    {
        await context.Response.SendFileAsync("wwwroot/index.html");
    });
});

app.Run();
