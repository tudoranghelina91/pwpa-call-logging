using DotNet.Testcontainers.Builders;
using MongoDB.Bson;
using MongoDB.Driver;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;
using PWPA.CallLogging.BackEnd.Infrastructure;
using Testcontainers.MongoDb;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

public class IntegrationTestsBase
{
    private MongoDbContainer? _container;

    private static IMongoDatabase GetDatabase()
    {
        IMongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CallsDatabaseSettings__ConnectionString"));
        var db = client.GetDatabase("pwpa-call-logging-db");
        return db;
    }

    protected async Task StartMongo()
    {
        Environment.SetEnvironmentVariable("CallsDatabaseSettings__ConnectionString", "mongodb://root:password@host.docker.internal:27017");

        _container = new MongoDbBuilder()
            .WithName("pwpa_call_logging_db")
            .WithUsername("root")
            .WithPassword("password")
            .WithPortBinding(27017, 27017)
            .Build();

        await _container.StartAsync();

        IMongoDatabase db = GetDatabase();
        db.CreateCollection("Calls");
        await Seed();
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

    protected async Task StopMongo()
    {
        await _container.DisposeAsync();
    }
}