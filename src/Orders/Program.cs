var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/orders/health", () =>
{
    return Results.Ok(new
    {
        status = "ok",
        service = "orders"
    });
});

app.Run();
