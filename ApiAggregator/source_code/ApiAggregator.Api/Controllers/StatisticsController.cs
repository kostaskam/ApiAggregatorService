using Microsoft.AspNetCore.Mvc;
using ApiAggregator.Api.Services;

namespace ApiAggregator.Api.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class StatisticsController : ControllerBase
    {
        private readonly RequestStatsService _statsService;

        public StatisticsController(RequestStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        public IActionResult GetStats()
        {
            return Ok(_statsService.GetRequestStatistics());
        }
    }
}
