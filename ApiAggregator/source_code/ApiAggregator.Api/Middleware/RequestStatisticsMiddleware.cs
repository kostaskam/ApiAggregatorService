using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApiAggregator.Api.Services;

namespace ApiAggregator.Api.Middleware
{
    public class RequestStatisticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestStatsService _statsService;

        public RequestStatisticsMiddleware(RequestDelegate next, RequestStatsService statsService)
        {
            _next = next;
            _statsService = statsService;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            var path = context.Request.Path.ToString();
            _statsService.LogRequest(path, stopwatch.ElapsedMilliseconds);
        }
    }
}
