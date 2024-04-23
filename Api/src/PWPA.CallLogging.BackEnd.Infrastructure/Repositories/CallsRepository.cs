using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;

namespace PWPA.CallLogging.BackEnd.Infrastructure.Repositories;

public class CallsRepository : ICallsRepository
{
    private readonly IMongoCollection<Call> _calls;

    public CallsRepository(IOptions<CallsDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);

        var database = client.GetDatabase(settings.Value.Database);

        _calls = database.GetCollection<Call>(settings.Value.CallsCollectionName);
    }

    public async Task AddCallAsync(string callerName, string address, string description)
    {
        await _calls.InsertOneAsync(new Call
        {
            CallerName = callerName,
            Address = address,
            Description = description
        });
    }

    public async Task<List<Call>> GetCallsAsync()
    {
        return (await _calls.FindAsync(_ => true)).ToList();
    }
}