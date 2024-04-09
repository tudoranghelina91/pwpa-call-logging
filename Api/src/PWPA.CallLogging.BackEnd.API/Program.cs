using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/calls", () =>
{
    return new[]
    {
        new
        {
            CallId = 1,
            CallerName = "Test"
        },
        new
        {
            CallId = 2,
            CallerName = "Test 2"
        }
    };
});

app.MapPost("/calls/add", () =>
{
    return new OkResult();
});

app.MapPut("/calls/{callId}", (int callId, object callData) =>
{
    return new OkObjectResult(callData);
});

app.Run();