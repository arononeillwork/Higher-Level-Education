using HigherLevelEducation.Core;
using HigherLevelEducation.Core.Models;
using Microsoft.Extensions.Time.Testing;

namespace HigherLevelEducation.Api.Core.Tests.Worksheets;

public class WorksheetGeneratorTests
{
    private readonly FakeTimeProvider _timeProvider = new();

    [Fact]
    public void Should_return_worksheet()
    {
        // assign - criteria
        var worksheetProvider = new WorksheetProvider();
        var criteria = new WorksheetCriteria(
            ClassLevel: 4,
            Topic: Topic.Addition,
            NumberOfQuestions: 5,
            DifficultyLevel: DifficultyLevel.Medium);
        
        worksheetProvider.AssignCriteria(criteria);
        
        // arrange - generate worksheet
        var worksheetGenerator = new WorksheetGenerator(worksheetProvider, _timeProvider);
        var worksheet = worksheetGenerator.AddWorksheet();

        // assert
        worksheet.Should().Match<Worksheet>(w =>
            w.Criteria.ClassLevel == criteria.ClassLevel &&
            w.Criteria.Topic == criteria.Topic &&
            w.Criteria.DifficultyLevel == criteria.DifficultyLevel);
    }
}