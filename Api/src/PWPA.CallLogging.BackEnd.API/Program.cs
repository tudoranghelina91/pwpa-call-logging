using MediatR;
using Microsoft.AspNetCore.Mvc;
using PWPA.CallLogging.BackEnd.ApplicationCore;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;
using PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;
using PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;
using PWPA.CallLogging.BackEnd.Infrastructure;
using PWPA.CallLogging.BackEnd.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(cfg =>
    cfg.AddMaps(ApplicationCoreAssemblyReference.Assembly));

builder.Services.Configure<CallsDatabaseSettings>(builder.Configuration.GetSection("CallsDatabaseSettings"));

builder.Services.AddTransient<ICallsRepository, CallsRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(ApplicationCoreAssemblyReference.Assembly));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        policy.WithOrigins([
            "http://localhost:4200",
            "https://localhost:4200",
            "http://localhost:8004",
            "https://localhost:8004",
            "http://pwpa-call-logging-vm.local:8004",
            "https://pwpa-call-logging-vm.local:8004",
            "http://192.168.1.133:8004",
            "https://192.168.1.133:8004",
        ]);
        policy.WithHeaders("Access-Control-Allow-Origin", "content-type");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowedOrigins");
app.UseHttpsRedirection();
app.MapGet("/calls", async (IMediator mediator) =>
{
    var request = new GetCallsRequest();
    var result = await mediator.Send(request);

    return result;
});

app.MapPost("/calls", async (IMediator mediator, [FromBody] AddCallRequest request) =>
{
    await mediator.Send(request);
    return;
});

app.MapPut("/calls/{callId}", (int callId, object callData) =>
{
    throw new NotImplementedException();
});

app.Run();