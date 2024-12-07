namespace HigherLevelEducation.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    [HttpGet("overview")]
    public IActionResult GetOverview()
    {
        // Logic to fetch user progress, topics covered, and topics left to learn
        return Ok();
    }

    [HttpGet("performance")]
    public IActionResult GetPerformance()
    {
        // Logic to fetch recommended study materials and weak areas
        return Ok();
    }
}