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
        //TODO: error handling
        // First action: return cached data if they exist
        if (_cache.TryGetValue("aggregatedData", out object cachedData))
        {
            return cachedData;
        }

        // Code resumed, this means that we have no cached data and thus we should initialize new requests.
        // Start running all three tasks
        var weatherTask = _weatherClient.GetWeatherDataAsync();
        var newsTask = _newsClient.GetNewsDataAsync();
        var cryptoTask = _cryptoClient.GetCryptoDataAsync();

        //await for all of the above tasks to finish.
        await Task.WhenAll(weatherTask, newsTask, cryptoTask);

        // Get the data from the clients
        var aggregatedData = new
        {
            Weather = await weatherTask,
            News = await newsTask,
            Crypto = await cryptoTask
        };

        //Set the cache for X minutes and return all data.
        //TODO: I should change from a hardcoded value to get it from a settings file)
        _cache.Set("aggregatedData", aggregatedData, TimeSpan.FromMinutes(5));
        return aggregatedData;
    }
}
