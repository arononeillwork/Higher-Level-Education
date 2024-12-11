using HigherLevelEducation.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HigherLevelEducation.Tests;

public class ApiFixture : WebApplicationFactory<IApiAssemblyMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var test = Directory.GetCurrentDirectory();
        var contentRoot = Path.GetFullPath(Path.Combine(
            Directory.GetCurrentDirectory(),
            "..", "..", "..", "..", "..", // Go back three directories
            "src", "HigherLevelEducation.Api" // Navigate into the target folder
        ));
        
        // Dynamically bind to a port in tests
        var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
        
        builder
            .UseContentRoot(contentRoot)
            .UseEnvironment("Development")
            .UseUrls($"http://*:{port}"); // Bind to the dynamic port
    }
}