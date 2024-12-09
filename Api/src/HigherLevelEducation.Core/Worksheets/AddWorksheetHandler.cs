using HigherLevelEducation.Core.Models;
using HigherLevelEducation.Core.Responses;

namespace HigherLevelEducation.Core.Worksheets;

public class AddWorksheetHandler
{
    private readonly WorksheetGenerator _worksheetGenerator;
    private readonly IWorksheetDataStore _worksheetDataStore;
    private readonly TimeProvider _timeProvider;

    public AddWorksheetHandler(
        WorksheetGenerator worksheetGenerator, 
        IWorksheetDataStore worksheetDataStore,
        TimeProvider timeProvider)
    {
        _worksheetGenerator = worksheetGenerator;
        _worksheetDataStore = worksheetDataStore;
        _timeProvider = timeProvider;
    }

    public async Task<Result<Worksheet>> HandleAsync(AddWorksheetRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            return Errors.MissingValue(nameof(request.Name));
        }
        var worksheet = _worksheetGenerator.AddWorksheet(request.Name);
        await _worksheetDataStore.AddAsync(worksheet, cancellationToken);
        
        return worksheet;
    }
}