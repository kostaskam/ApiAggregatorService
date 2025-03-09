using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class WeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;
    private readonly string _weatherApiUrl;

    public WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _weatherApiUrl = configuration["ExternalAPIs:WeatherAPI"];  // Access the URL from appsettings.json
    }

    public async Task<object> GetWeatherDataAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(_weatherApiUrl);
            return JsonSerializer.Deserialize<object>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weather API call failed");
            return null; // Fallback
        }
    }
}
