namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

public class IntegrationTestsBase : IClassFixture<IntegrationTestsFactory>
{
    private readonly IntegrationTestsFactory _factory;

    protected IntegrationTestsBase(IntegrationTestsFactory factory)
    {
        _factory = factory;
    }
}