using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using TheEmployeeAPI.Employees;

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
    
    [Fact]
    public async Task GetEmployeeById_ReturnsOkResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/employees/1");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateEmployee_ReturnsCreatedResult()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/employees", new Employee { FirstName = "John", LastName = "Doe", SocialSecurityNumber = "123-33-4322"});

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateEmployee_ReturnsBadRequestResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var invalidEmployee = new CreateEmployeeRequest();
        // Act
        var response = await client.PostAsJsonAsync("/employees", invalidEmployee);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Contains("FirstName", problemDetails.Errors.Keys);
        Assert.Contains("LastName", problemDetails.Errors.Keys);
        Assert.Contains("'First Name' must not be empty.", problemDetails.Errors["FirstName"]);
        Assert.Contains("'Last Name' must not be empty.", problemDetails.Errors["LastName"]);

    }


    [Fact]
    public async Task UpdateEmployee_ReturnsOkResult()
    {
        var client = _factory.CreateClient();
        var response = await client.PutAsJsonAsync("/employees/1", new Employee
        {
            FirstName = "Joelito",
            LastName = "Koreso",
            SocialSecurityNumber = "11244-334-5"
        });

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsBadRequestResult()
    {
        var client = _factory.CreateClient();
        var response = await client.PutAsJsonAsync("/employees/9999", (new Employee
        {
            FirstName = "Joelito",
            LastName = "Koreso",
            SocialSecurityNumber = "11244-334-5"
        }));
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}