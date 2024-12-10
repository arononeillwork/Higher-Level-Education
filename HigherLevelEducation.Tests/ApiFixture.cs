using HigherLevelEducation.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HigherLevelEducation.Tests;

public class ApiFixture : WebApplicationFactory<IApiAssemblyMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var contentRoot = Path.GetFullPath(Path.Combine(
            Directory.GetCurrentDirectory(),
            "..", "..", "..", "..", // Go back three directories
            "Api", "src", "HigherLevelEducation.Api" // Navigate into the target folder
        ));
        
        builder
            .UseContentRoot(contentRoot)
            .UseEnvironment("Development");
    }
}