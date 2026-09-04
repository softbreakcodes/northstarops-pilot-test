using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

public sealed class OrdersReadinessEndpointTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersReadinessEndpointTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrdersReadiness_ReturnsOkWithExactExpectedResponse()
    {
        using var response = await _client.GetAsync("/orders/readiness");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(content);

        var properties = json.RootElement
            .EnumerateObject()
            .ToArray();

        Assert.Equal(2, properties.Length);

        Assert.Equal(
            "ready",
            json.RootElement.GetProperty("status").GetString());

        Assert.Equal(
            "orders",
            json.RootElement.GetProperty("service").GetString());
    }
}
