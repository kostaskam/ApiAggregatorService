using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/aggregated")]
public class AggregatedController : ControllerBase
{
    private readonly AggregationService _aggregationService;

    public AggregatedController(AggregationService aggregationService)
    {
        _aggregationService = aggregationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAggregatedData()
    {
        var result = await _aggregationService.GetAggregatedDataAsync();
        return Ok(result);
    }
}
