namespace HigherLevelEducation.Core.Models;

public record WorksheetCriteria(
    int ClassLevel,
    Topic Topic,
    DifficultyLevel DifficultyLevel,
    int? NumberOfQuestions = 10);