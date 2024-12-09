using HigherLevelEducation.Core.Models;

namespace HigherLevelEducation.Core;

public class WorksheetProvider
{
    private WorksheetCriteria? _criteria;
    
    public void AssignCriteria(WorksheetCriteria criteria)
    {
        _criteria = criteria;
    }
    
    public WorksheetCriteria GetWorksheetCrtieria()
    {
        return _criteria;
    }
}