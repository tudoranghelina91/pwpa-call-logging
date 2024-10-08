using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using MongoDB.Driver;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;
using Testcontainers.MongoDb;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

[CollectionDefinition(nameof(IntegrationTestsCollection))]
public sealed class IntegrationTestsCollection : ICollectionFixture<WebApplicationFactory<Program>>
{
    //private async Task Seed()
    //{
    //    IMongoDatabase database = GetDatabase();
    //    var collection = database.GetCollection<Call>("Calls");
    //    await collection.InsertManyAsync(
    //    [
    //        new Call
    //        {
    //            Id = ObjectId.GenerateNewId().ToString(),
    //            Address = nameof(Call.Address) + 1,
    //            CallerName = nameof(Call.CallerName) + 1,
    //            Description = nameof(Call.Description) + 1
    //        },
    //        new Call
    //        {
    //            Id = ObjectId.GenerateNewId().ToString(),
    //            Address = nameof(Call.Address) + 2,
    //            CallerName = nameof(Call.CallerName) + 2,
    //            Description = nameof(Call.Description) + 2
    //        }
    //    ]);
    //}

    //private static IMongoDatabase GetDatabase()
    //{
    //    IMongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CallsDatabaseSettings__ConnectionString"));
    //    var db = client.GetDatabase("pwpa-call-logging-db");
    //    return db;
    //}
}