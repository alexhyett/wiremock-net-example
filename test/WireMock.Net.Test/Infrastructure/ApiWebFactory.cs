using System.IO;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

using WireMock.Net.Api;

namespace WireMock.Net.Test.Infrastructure
{
    public class ApiWebFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile("appsettings.test.json", false, false)
                    .AddEnvironmentVariables();
            })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseKestrel()
            .UseStartup<Startup>();
    }
}