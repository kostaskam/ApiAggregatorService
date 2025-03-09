using ApiAggregator.Api.Clients;
using ApiAggregator.Api.Middleware;
using ApiAggregator.Api.Services;

var builder = WebApplication.CreateBuilder(args);

//Add the configuration 
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddScoped<AggregationService>();
builder.Services.AddScoped<WeatherApiClient>();
builder.Services.AddScoped<NewsApiClient>();
builder.Services.AddScoped<CryptoApiClient>();
builder.Services.AddSingleton<RequestStatsService>();

var app = builder.Build();

// Middleware
app.UseMiddleware<RequestStatisticsMiddleware>(); // Add middleware
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
