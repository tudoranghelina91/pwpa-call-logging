using MassTransit.Testing;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

public abstract class IntegrationTestsBase : IClassFixture<IntegrationTestsFactory>
{
    private readonly IntegrationTestsFactory _factory;
    protected VerifySettings DatabaseVerifyWriteSettings;
    protected VerifySettings RabbitMQVerifySettings;
    protected ITestHarness Harness;

    protected IntegrationTestsBase(IntegrationTestsFactory factory)
    {
        _factory = factory;
        //using var scope = factory.Services.CreateScope();
        Harness = factory.Services.GetService<ITestHarness>();

        DatabaseVerifyWriteSettings = new VerifySettings();
        DatabaseVerifyWriteSettings.UseDirectory("./Verify");

        RabbitMQVerifySettings = new VerifySettings();
        RabbitMQVerifySettings.UseDirectory("./Verify");
        RabbitMQVerifySettings.UseFileName("BasicTests.Should_Log_Call_Queue");
    }
}