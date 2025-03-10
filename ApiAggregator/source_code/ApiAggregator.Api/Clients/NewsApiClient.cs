using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiAggregator.Api.Clients
{
    public class NewsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsApiClient> _logger;
        private readonly string _newsApiUrl;

        public NewsApiClient(HttpClient httpClient, ILogger<NewsApiClient> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _newsApiUrl = configuration["ExternalAPIs:NewsAPI"];  // Access the URL from appsettings.json
        }

        public async Task<object> GetNewsDataAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_newsApiUrl);
                return JsonSerializer.Deserialize<object>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "News API call failed");
                //TODO: Log error to Database too.
                return null; // Fallback
            }
        }
    }
}
