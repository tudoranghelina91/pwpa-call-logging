using PWPA.CallLogging.BackEnd.ApplicationCore.Models;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;

public interface ICallsRepository
{
    Task<List<Call>> GetCallsAsync();

    Task AddCallAsync(string callerName, string address, string description);
}