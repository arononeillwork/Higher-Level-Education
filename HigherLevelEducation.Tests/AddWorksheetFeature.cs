using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HigherLevelEducation.Core;
using HigherLevelEducation.Core.Models;

namespace HigherLevelEducation.Tests;

public class AddWorksheetFeature(ApiFixture fixture) : IClassFixture<ApiFixture>
{
    private const int ClassLevel = 4;
    private const int NumberOfQuestions = 8;
    
    private readonly HttpClient _client = fixture.CreateClient();
    private readonly WorksheetCriteria _criteria = new(
        ClassLevel: ClassLevel,
        Topic: Topic.Addition,
        NumberOfQuestions: NumberOfQuestions,
        DifficultyLevel: DifficultyLevel.Medium);

    [Fact]
    public async Task Given_worksheet_should_return_ok()
    {
        var response = await _client.PostAsJsonAsync("api/worksheets", new AddWorksheetRequest(_criteria, "Test Worksheet"));
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var addWorksheetResponse = await response.Content.ReadFromJsonAsync<Worksheet>();
        addWorksheetResponse!.Questions.Count.Should().Be(NumberOfQuestions);
    }
}