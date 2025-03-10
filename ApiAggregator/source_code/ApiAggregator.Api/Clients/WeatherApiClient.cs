using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class WeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;
    private readonly string _weatherApiUrl;
    public readonly double _Latitude;
    public readonly double _Longitude;

    public WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _weatherApiUrl = configuration["ExternalAPIs:WeatherAPI"];  // Access the URL from appsettings.json
        
        /* Following are Hardcoded values of Thessaloniki.
        * TODO: Should change from hardcoded values to the lat+long from an api call that gets Users current location.
        * will need to create a "Location" Model for that.
        */
        _Latitude = 40.64;  
        _Longitude = 22.94; 

    }

    public async Task<object> GetWeatherDataAsync()
    {
        try
        {
            var requestUri = $"{_weatherApiUrl}?latitude={_Latitude}&longitude={_Longitude}&hourly=temperature_2m&timezone=auto";
            var response = await _httpClient.GetStringAsync(_weatherApiUrl);
            return JsonSerializer.Deserialize<object>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weather API call failed");
            //TODO: Log error to Database too.
            return null; // Fallback
        }
    }
}
