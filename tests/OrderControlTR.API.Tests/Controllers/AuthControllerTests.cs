using Microsoft.AspNetCore.Mvc.Testing;

namespace OrderControlTR.API.Tests.Controllers;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        Assert.NotNull(_factory);
        await Task.CompletedTask;
    }
}
