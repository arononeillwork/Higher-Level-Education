using HigherLevelEducation.Api.Core.Tests.TestDoubles;
using HigherLevelEducation.Core;
using HigherLevelEducation.Core.Models;
using HigherLevelEducation.Core.Worksheets;
using Microsoft.Extensions.Time.Testing;

namespace HigherLevelEducation.Api.Core.Tests.Worksheets;

public class AddWorksheetTests
{
    private readonly WorksheetCriteria _criteria;
    private readonly AddWorksheetHandler _handler;
    private readonly AddWorksheetRequest _request;
    private readonly InMemoryWorksheetDataStore _worksheetDataStore = new();
    private readonly FakeTimeProvider _timeProvider;

    public AddWorksheetTests()
    {
        _criteria = new WorksheetCriteria(
            ClassLevel: 4,
            Topic: Topic.Addition,
            NumberOfQuestions: 5,
            DifficultyLevel: DifficultyLevel.Medium);
        
        var worksheetProvider = new WorksheetProvider();
        worksheetProvider.AssignCriteria(_criteria);
        
        _timeProvider = new FakeTimeProvider();
        var worksheetGenerator = new WorksheetGenerator(worksheetProvider, _timeProvider);
        _handler = new AddWorksheetHandler(worksheetGenerator, _worksheetDataStore, _timeProvider);
        
        _request = new AddWorksheetRequest(_criteria, "Test Worksheet");
    }
    
    [Fact]
    public async Task Should_return_worksheet()
    {
        // Request created in constructor
        var response = await _handler.HandleAsync(_request, cancellationToken: default);

        response.Value!.Questions.Should().NotBeNull();
        response.Value.Criteria.Should().BeEquivalentTo(_criteria);
    }
    
    [Fact]
    public async Task Should_save_worksheet()
    {
        // Request created in constructor
        var response = await _handler.HandleAsync(_request, cancellationToken: default);

        // assert
        _worksheetDataStore.Should().ContainKey(response.Value!.Id);
    }
    
    [Fact]
    public async Task Should_save_worksheet_with_created_modified_properties()
    {
        // Request created in constructor
        var response = await _handler.HandleAsync(_request, cancellationToken: default);

        // assert
        _worksheetDataStore.Should().ContainKey(response.Value!.Id);
        _worksheetDataStore[response.Value.Id].CreatedBy.Should().Be(1); // ToDo: Get current user
        _worksheetDataStore[response.Value.Id].CreatedOn.Should().Be(_timeProvider.GetUtcNow());
        _worksheetDataStore[response.Value.Id].ModifiedOn.Should().Be(null);
    }

    [Fact]
    public async Task Should_return_error_if_name_is_empty()
    {
        var request = new AddWorksheetRequest(_criteria, string.Empty);
        
        var response = await _handler.HandleAsync(request, cancellationToken: default);
        
        // assert
        response.Succeeded.Should().BeFalse();
        response.Error.Code.Should().Be("missing_data");
        response.Error.Description.Should().Be("Name is required");
    }
}