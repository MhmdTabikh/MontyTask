using MontyTask.Data.Models;
using MontyTask.Extensions;
using MontyTask.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCustomSwagger();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);


var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseCustomSwagger();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using var scope = app.Services.CreateScope();
try
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    await DatabaseSeed.SeedAsync(dbContext, passwordHasher);
}
catch (Exception ex)
{
    var dbLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    dbLogger.LogError(ex, "An error occured while applying migrations");
}

app.Run();
