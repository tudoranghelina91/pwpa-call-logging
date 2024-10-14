using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using MongoDB.Bson;
using MongoDB.Driver;
using PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;
using PWPA.CallLogging.BackEnd.Infrastructure;
using Testcontainers.MongoDb;
using Testcontainers.RabbitMq;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

public class IntegrationTestsFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string ConnectionString = "mongodb://root:password@localhost:27017";

    private readonly MongoDbContainer _mongoDB = new MongoDbBuilder()
            .WithName("pwpa_call_logging_db")
            .WithUsername("root")
            .WithPassword("password")
            .WithPortBinding(27017, 27017)
            .Build();

    private readonly RabbitMqContainer _rabbitMQ = new RabbitMqBuilder()
        .WithName("pwpa_message_bus")
        .WithUsername("root")
        .WithPassword("password")
        .WithHostname("pwpamessagebus")
        .WithPortBinding(8005, 15672)
        .WithPortBinding(8006, 5672)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddMassTransitTestHarness(x =>
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", 8006, "/", h =>
                                    {
                                        h.Username("root");
                                        h.Password("password");
                                    });

                    cfg.ExchangeType = "direct";

                    cfg.Publish<AddCallRequest>(x =>
                    {
                        x.Exclude = true;
                        x.ExchangeType = "direct";
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            services.Configure<CallsDatabaseSettings>(cfg =>
            {
                cfg.ConnectionString = ConnectionString;
                cfg.Database = "pwpa-call-logging-db";
                cfg.CallsCollectionName = "Calls";
            });
        });
    }

    private async Task Seed()
    {
        IMongoDatabase database = GetDatabase();
        var collection = database.GetCollection<Call>("Calls");
        await collection.InsertManyAsync(
        [
            new Call
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Address = nameof(Call.Address) + 1,
                CallerName = nameof(Call.CallerName) + 1,
                Description = nameof(Call.Description) + 1
            },
            new Call
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Address = nameof(Call.Address) + 2,
                CallerName = nameof(Call.CallerName) + 2,
                Description = nameof(Call.Description) + 2
            }
        ]);
    }

    private static IMongoDatabase GetDatabase()
    {
        IMongoClient client = new MongoClient(ConnectionString);
        var db = client.GetDatabase("pwpa-call-logging-db");
        return db;
    }

    public async Task InitializeAsync()
    {
        await _mongoDB.StartAsync();
        await _rabbitMQ.StartAsync();
        await Seed();

        return;
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        Task[] tasks =
        {
            _rabbitMQ.StopAsync(),
            _mongoDB.StopAsync()
        };

        return Task.WhenAll(tasks);
    }
}