namespace HigherLevelEducation.Core.Models;

public record WorksheetCriteria(
    int ClassLevel,
    Topic Topic,
    int NumberOfQuestions,
    DifficultyLevel DifficultyLevel);