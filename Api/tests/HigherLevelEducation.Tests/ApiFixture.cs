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
        
        builder
            .UseContentRoot(contentRoot)
            .UseEnvironment("Development");
    }
}