using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

public sealed class OrdersHealthEndpointTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersHealthEndpointTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrdersHealth_ReturnsOkWithExpectedResponse()
    {
        using var response = await _client.GetAsync("/orders/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(content);

        Assert.Equal(
            "ok",
            json.RootElement.GetProperty("status").GetString());

        Assert.Equal(
            "orders",
            json.RootElement.GetProperty("service").GetString());
    }
}
