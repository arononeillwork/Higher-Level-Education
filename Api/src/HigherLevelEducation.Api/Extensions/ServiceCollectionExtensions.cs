using HigherLevelEducation.Core;
using HigherLevelEducation.Core.Models;
using HigherLevelEducation.Core.Worksheets;

namespace HigherLevelEducation.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorksheetFeature(this IServiceCollection services)
    {
        services.AddScoped<AddWorksheetHandler>();
        services.AddSingleton<WorksheetProvider>(_ =>
        {
            var worksheetProvider = new WorksheetProvider();
            var criteria = new WorksheetCriteria(
                ClassLevel: 4,
                Topic: Topic.Addition,
                DifficultyLevel: DifficultyLevel.Medium);
            worksheetProvider.AssignCriteria(criteria);
            return worksheetProvider;
        });
        services.AddScoped<WorksheetGenerator>();
        
        services.AddSingleton<IWorksheetDataStore, InMemoryWorksheetDataStore>();

        return services;
    }

}

public class InMemoryWorksheetDataStore : Dictionary<int, Worksheet>, IWorksheetDataStore
{
    public Task AddAsync(Worksheet worksheet, CancellationToken cancellationToken)
    {
        // ToDo: create new key each time - make abstract
        Add(1, worksheet);
        return Task.CompletedTask;
    }
}