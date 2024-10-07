using Docker.DotNet.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;
using PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;
using System.Net.Http.Json;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

public class BasicTests
    : IntegrationTestsBase, IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/calls")]
    public async Task Should_Return_All_Logged_Calls(string url)
    {
        await StartMongo();
        var client = _factory.CreateClient();
        var response = await client.GetFromJsonAsync<IEnumerable<GetCallsResponse>>(url);
        await Verify(response);
        await StopMongo();
    }

    [Theory]
    [InlineData("/calls")]
    public async Task Should_Log_Call(string url)
    {
        await StartMongo();

        var client = _factory.CreateClient();
        var _ = await client.PostAsJsonAsync(url, new AddCallRequest
        (
            nameof(AddCallRequest.CallerName) + 3,
            nameof(AddCallRequest.Address) + 3,
            nameof(AddCallRequest.Description) + 3
        ));

        var response = await client.GetFromJsonAsync<IEnumerable<GetCallsResponse>>(url);
        await Verify(response);
        await StopMongo();
    }
}