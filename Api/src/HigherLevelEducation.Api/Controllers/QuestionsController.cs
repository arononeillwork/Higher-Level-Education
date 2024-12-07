namespace HigherLevelEducation.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
    [HttpPost("quiz")]
    public IActionResult CreateQuiz([FromBody] CustomQuiz quiz)
    {
        // Logic to allow creation and storage of user-specific quizzes
        return Ok();
    }
}

public record CustomQuiz
{
    // needs to change to enum
    [Required]
    public string Topic { get; init; }

    [Range(1, 5)]
    public int Difficulty { get; init; }
    
    [Range(1, 15)]
    public int NumberOfQuestions { get; init; }
}