using HigherLevelEducation.Core.Interfaces;

namespace HigherLevelEducation.Core.Models;

public class Worksheet : Entity
{
    public Worksheet (WorksheetCriteria criteria)
    {
        Id = 1;
        Criteria = criteria;
    }
    
    public WorksheetCriteria Criteria { get; }
    public IList<string> Questions { get; init; }
}

public record AddWorksheetRequest(
    WorksheetCriteria Criteria,
    string Name);