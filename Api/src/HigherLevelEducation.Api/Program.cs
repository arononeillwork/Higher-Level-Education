using Azure.Identity;

using HigherLevelEducation.Api.Extensions;
using HigherLevelEducation.Core.Models;
using HigherLevelEducation.Core.Worksheets;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVaultName"];
if (!string.IsNullOrEmpty(keyVaultName))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{keyVaultName}.vault.azure.net/"),
        new DefaultAzureCredential());
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddWorksheetFeature();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost(pattern: "/api/worksheets",
    async (AddWorksheetHandler handler, AddWorksheetRequest request, CancellationToken cancellationToken) =>
    {
        var result = await handler.HandleAsync(request, cancellationToken);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Created(
            uri: $"/api/worksheets/{result.Value!.Id}",
            value: result.Value
        );
    });

app.Run();