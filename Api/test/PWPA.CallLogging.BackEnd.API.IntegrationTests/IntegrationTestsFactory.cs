using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

    //    builder.Services.AddMassTransit(x =>
    //        {
    //            x.UsingRabbitMq((ctx, cfg) =>
    //            {
    //                string? host = Environment.GetEnvironmentVariable("RabbitMQ__Host");
    //    ushort port = (ushort)Convert.ToInt32(Environment.GetEnvironmentVariable("RabbitMQ__Port"));
    //    string? user = Environment.GetEnvironmentVariable("RabbitMQ__User");
    //    string? pass = Environment.GetEnvironmentVariable("RabbitMQ__Password");

    //                if (host is null || user is null || port == 0 || pass is null)
    //                {
    //                    throw new ConfigurationException("RabbitMQ");
    //}

    //cfg.Host(host, port, "/", h =>
    //                {
    //                    h.Username(user);
    //                    h.Password(pass);
    //                });

    //cfg.ExchangeType = "direct";

    //cfg.Publish<AddCallRequest>(x =>
    //{
    //    x.Exclude = true;
    //    x.ExchangeType = "direct";
    //});

    //cfg.ConfigureEndpoints(ctx);
    //            });
    //        });

    private readonly MongoDbContainer _container = new MongoDbBuilder()
            .WithName("pwpa_call_logging_db")
            .WithUsername("root")
            .WithPassword("password")
            .WithPortBinding(27017, 27017)
            .Build();

    private readonly RabbitMqContainer _rabbitMq = new RabbitMqBuilder()
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
        await _container.StartAsync();
        await _rabbitMq.StartAsync();
        await Seed();

        return;
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _container.StopAsync();
    }
}