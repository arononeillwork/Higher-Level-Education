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
        builder
            .UseContentRoot(@"C:\Users\Aron O'Neill\Documents\EducationApp\Rough\Dometrain\Higher-Level-Education\Api\src\HigherLevelEducation.Api")
            .UseEnvironment("Development");
    }
}