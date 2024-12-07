namespace HigherLevelEducation.Api.Controllers;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/leaderboard")]
public class LeaderboardController : ControllerBase
{
    [HttpGet]
    public IActionResult GetLeaderboard()
    {
        // Logic to fetch and update leaderboard data
        return Ok();
    }
}