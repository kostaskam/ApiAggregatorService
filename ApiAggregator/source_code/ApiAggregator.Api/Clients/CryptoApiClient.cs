using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApiAggregator.Api.Clients
{
    public class CryptoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CryptoApiClient> _logger;
        private readonly string _cryptoApiUrl;

        public CryptoApiClient(HttpClient httpClient, ILogger<CryptoApiClient> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _cryptoApiUrl = configuration["ExternalAPIs:CryptoAPI"];  // Access the URL from appsettings.json
        }

        public async Task<object> GetCryptoDataAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_cryptoApiUrl);
                return JsonSerializer.Deserialize<object>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Crypto API call failed");
                
                //TODO: Log error to Database too.
                return null; // Fallback
            }
        }
    }
}
