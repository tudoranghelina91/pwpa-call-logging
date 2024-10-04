using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;

namespace PWPA.CallLogging.BackEnd.Infrastructure.UnitTests;

public class CallsRepositoryTests
{
    public CallsRepositoryTests()
    {
    }

    private readonly ICallsRepository _sut;

    [Fact]
    public void GetCallsAsync_Should_Return_Calls_List()
    {
    }
}