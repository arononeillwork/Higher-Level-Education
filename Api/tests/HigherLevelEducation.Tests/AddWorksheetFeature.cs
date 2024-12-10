using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HigherLevelEducation.Core;
using HigherLevelEducation.Core.Models;

namespace HigherLevelEducation.Tests;

public class AddWorksheetFeature : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;
    
    private const int ClassLevel = 4;
    private const int NumberOfQuestions = 10;
    
    private readonly WorksheetCriteria _criteria = new(
        ClassLevel: ClassLevel,
        Topic: Topic.Addition,
        NumberOfQuestions: NumberOfQuestions,
        DifficultyLevel: DifficultyLevel.Medium);
    
    public AddWorksheetFeature(ApiFixture fixture)
    {
        _client = fixture.CreateClient();
    }
    
    [Fact]
    public async Task Given_worksheet_should_return_ok()
    {
        var response = await _client.PostAsJsonAsync("api/worksheets", new AddWorksheetRequest(_criteria, "Test Worksheet"));
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var addWorksheetResponse = await response.Content.ReadFromJsonAsync<Worksheet>();
        addWorksheetResponse!.Questions.Count.Should().Be(NumberOfQuestions);
    }
}