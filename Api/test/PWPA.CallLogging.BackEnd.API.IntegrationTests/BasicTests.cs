using PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;
using PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

public class BasicTests : IntegrationTestsBase
{
    private readonly IntegrationTestsFactory _factory;

    public BasicTests(IntegrationTestsFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/calls")]
    public async Task Should_Return_All_Logged_Calls(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetFromJsonAsync<IEnumerable<GetCallsResponse>>(url);
        await Verify(response);
    }

    [Theory]
    [InlineData("/calls")]
    public async Task Should_Log_Call(string url)
    {
        var client = _factory.CreateClient();
        var _ = await client.PostAsJsonAsync(url, new AddCallRequest
        (
            nameof(AddCallRequest.CallerName) + 3,
            nameof(AddCallRequest.Address) + 3,
            nameof(AddCallRequest.Description) + 3
        ));

        var response = await client.GetFromJsonAsync<IEnumerable<GetCallsResponse>>(url);
        await Verify(response);
    }
}