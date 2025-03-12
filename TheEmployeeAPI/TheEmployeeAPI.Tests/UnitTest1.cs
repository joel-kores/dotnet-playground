using Microsoft.AspNetCore.Mvc.Testing;

namespace TheEmployeeAPI.Tests;

public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllEmployees_ReturnOkResult()
    {
        HttpClient client = _factory.CreateClient();
        var response = await client.GetAsync("/employees");
        response.EnsureSuccessStatusCode();
    }
}