using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ApiAggregator.Api.Services
{
    public class RequestStatsService
    {
        private readonly ConcurrentDictionary<string, List<long>> _apiResponseTimes = new();

        public void LogRequest(string apiName, long responseTime)
        {
            if (!_apiResponseTimes.ContainsKey(apiName))
            {
                _apiResponseTimes[apiName] = new List<long>();
            }

            _apiResponseTimes[apiName].Add(responseTime);
        }

        public object GetRequestStatistics()
        {
            return _apiResponseTimes.ToDictionary(
                entry => entry.Key,
                entry => new
                {
                    TotalRequests = entry.Value.Count,
                    AverageResponseTime = entry.Value.Count > 0 ? entry.Value.Average() : 0,
                    PerformanceBuckets = new
                    {
                        Fast = entry.Value.Count(t => t < 100),
                        Average = entry.Value.Count(t => t >= 100 && t <= 200),
                        Slow = entry.Value.Count(t => t > 200)
                    }
                }
            );
        }
    }
}
