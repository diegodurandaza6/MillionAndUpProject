using Properties.Ms.Domain;
using Properties.Ms.AdapterOutRepository;
using Properties.Ms.AdapterOutRepository.SqlServer;
using Properties.Ms.AdapterInHttp.Extensions;

string projetcName = "Million And Up Project";

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();
