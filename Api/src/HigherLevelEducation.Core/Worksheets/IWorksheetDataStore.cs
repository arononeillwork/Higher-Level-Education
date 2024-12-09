using HigherLevelEducation.Core.Models;

namespace HigherLevelEducation.Core.Worksheets;

public interface IWorksheetDataStore
{
    Task AddAsync(Worksheet worksheet, CancellationToken cancellationToken);
}