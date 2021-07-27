using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace RpnApiTests
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<StartUpForTest>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .ConfigureAppConfiguration((configuration) =>
                {
                    configuration.AddJsonFile("appsettings.test.json");
                })
                .UseStartup<StartUpForTest>();
        }
    }
}
