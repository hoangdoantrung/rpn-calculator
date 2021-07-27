using Microsoft.Extensions.Configuration;
using RpnApi;

namespace RpnApiTests
{
    /// <summary>
    /// Mock all external dependencies like Database or others APIs.
    /// </summary>
    public class StartUpForTest : Startup
    {
        public StartUpForTest(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
