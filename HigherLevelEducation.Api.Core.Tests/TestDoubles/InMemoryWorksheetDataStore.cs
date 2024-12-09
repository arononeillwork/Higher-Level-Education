using HigherLevelEducation.Core.Models;
using HigherLevelEducation.Core.Worksheets;

namespace HigherLevelEducation.Api.Core.Tests.TestDoubles;

public class InMemoryWorksheetDataStore : Dictionary<int, Worksheet>, IWorksheetDataStore
{
    public Task AddAsync(Worksheet worksheet, CancellationToken cancellationToken)
    {
        // ToDo: create new key each time - make abstract
        Add(1, worksheet);
        return Task.CompletedTask;
    }
}