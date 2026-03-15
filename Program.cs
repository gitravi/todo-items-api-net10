using Microsoft.EntityFrameworkCore;
using TodoApi.DataAccess;
using NLog.Web;
using NLog;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<TodoContext>(options =>
        options.UseInMemoryDatabase("TodoList"));

    // Add services to the container.
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names as defined in the model
    });

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<TodoDbContext>(options =>
        options.UseInMemoryDatabase("TodoList"));

    builder.Logging.ClearProviders();
    // Register NLog as the logging provider
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUi(options =>
        {
        options.DocumentPath = "/openapi/v1.json"; 
        });
    }

    app.UseHttpsRedirection();

    // Serve default files (like index.html) and static files (like CSS, JS, images) from the wwwroot folder.
    app.UseDefaultFiles();

    // Enable serving static files such as CSS, JavaScript, and images from the wwwroot folder.
    app.UseStaticFiles();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}