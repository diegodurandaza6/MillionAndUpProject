using Properties.Ms.Domain;
using Properties.Ms.AdapterOutRepository;
using Properties.Ms.AdapterOutRepository.SqlServer;
using Properties.Ms.AdapterInHttp.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

string projetcName = "Million And Up Project";
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();

// Add services to the container.
builder.Services.AddJwtCustomized(configuration);
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
builder.Services.AddControllers();
builder.Services.AddDomain();
builder.Services.AddPersistence(configuration);
builder.Services.AddRepository();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenCustomized(projetcName);
builder.Services.ConfigureVersioning();

var app = builder.Build();

app.AllowSwaggerToListApiVersions(projetcName);

app.MapHealthChecks("/health");
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}
);
app.UseHealthChecksUI(config =>
{
    config.UIPath = "/health-ui";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MigrateDatabase();
app.Run();
