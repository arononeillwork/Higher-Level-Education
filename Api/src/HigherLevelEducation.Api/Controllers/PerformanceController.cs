namespace HigherLevelEducation.Api.Controllers;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/performance")]
public class PerformanceController : ControllerBase
{
    [HttpGet]
    public IActionResult GetPerformance()
    {
        // Logic to fetch topic-specific performance data for visualizations
        return Ok();
    }
}