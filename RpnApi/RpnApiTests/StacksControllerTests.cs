using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using RpnModels;
using RpnServices;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RpnApiTests
{
    public class StacksControllerTests : IClassFixture<CustomWebApplicationFactory<StartUpForTest>>
    {
        private readonly CustomWebApplicationFactory<StartUpForTest> _factory;

        public StacksControllerTests(CustomWebApplicationFactory<StartUpForTest> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task Should_create_new_stack()
        {
            // Given a client
            var httpClient = _factory
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
                {
                    services.AddTransient(_ =>
                    {
                        var mockService = Substitute.For<IStackService>();
                        mockService.CreateStack().Returns(10);
                        return mockService;
                    });
                }))
                .CreateClient();

            // When client create a new stack
            var response = await httpClient.PostAsync("/api/v1/Stacks", null);

            // Then a new stack is created with new id
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            Assert.Equal(10, JsonConvert.DeserializeObject<int>(json));
        }


        [Fact]
        public async Task Create_operator_should_return_stack_with_new_result()
        {
            // Given a client and a new stack with 5 and 6 as operands
            var httpClient = _factory
                .CreateClient();

            var response = await httpClient.PostAsync("/api/v1/Stacks", null);
            var json = await response.Content.ReadAsStringAsync();
            var stackId = JsonConvert.DeserializeObject<int>(json);
            await httpClient.PatchAsync($"/api/v1/Stacks/{stackId}?operand=5", null);
            await httpClient.PatchAsync($"/api/v1/Stacks/{stackId}?operand=6", null);

            // When create '+' operator
            response = await httpClient.PostAsync($"/api/v1/Stacks/{stackId}?op=add", null);

            // Then a new stack is created with new id
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<StackModel>(json);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.Operands.Count);
            Assert.Equal(11, result.Operands[0]);
        }
    }
}
