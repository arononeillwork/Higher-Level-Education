using HigherLevelEducation.Core.Models;

namespace HigherLevelEducation.Core;

public class WorksheetGenerator
{
    private readonly WorksheetProvider _worksheetProvider;
    private readonly TimeProvider _timeProvider;

    public WorksheetGenerator(WorksheetProvider worksheetProvider, TimeProvider timeProvider)
    {
        _worksheetProvider = worksheetProvider;
        _timeProvider = timeProvider;
    }

    public Worksheet AddWorksheet(
        string name = "Test Worksheet")
    {
        var criteria = _worksheetProvider.GetWorksheetCrtieria();
        var questions = QuestionsApi.CreateQuestions(new QuestionsApi.CreateQuestionsModel(
            criteria.ClassLevel, 
            Subject.Maths, 
            criteria.Topic, 
            criteria.DifficultyLevel, 
            criteria.NumberOfQuestions));

        var worksheet = new Worksheet(criteria)
        {
            Name = name,
            Questions = questions,
            CreatedOn = _timeProvider.GetUtcNow(),
            CreatedBy = 1 // ToDo: Get current user
        };

        return worksheet;
    }
}