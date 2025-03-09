using ApiAggregator.Api.Clients;
using Microsoft.Extensions.Caching.Memory;
public class AggregationService
{
    private readonly WeatherApiClient _weatherClient;
    private readonly NewsApiClient _newsClient;
    private readonly CryptoApiClient _cryptoClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AggregationService> _logger;

    public AggregationService(WeatherApiClient weather, NewsApiClient news, CryptoApiClient crypto, IMemoryCache cache, ILogger<AggregationService> logger)
    {
        _weatherClient = weather;
        _newsClient = news;
        _cryptoClient = crypto;
        _cache = cache;
        _logger = logger;
    }

    public async Task<object> GetAggregatedDataAsync()
    {
        if (_cache.TryGetValue("aggregatedData", out object cachedData))
        {
            return cachedData;
        }

        var weatherTask = _weatherClient.GetWeatherDataAsync();
        var newsTask = _newsClient.GetNewsDataAsync();
        var cryptoTask = _cryptoClient.GetCryptoDataAsync();

        await Task.WhenAll(weatherTask, newsTask, cryptoTask);

        var aggregatedData = new
        {
            Weather = await weatherTask,
            News = await newsTask,
            Crypto = await cryptoTask
        };

        _cache.Set("aggregatedData", aggregatedData, TimeSpan.FromMinutes(5));
        return aggregatedData;
    }
}
