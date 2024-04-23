namespace PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;

public record GetCallsResponse(string? CallerName, string? Address, string? Description);