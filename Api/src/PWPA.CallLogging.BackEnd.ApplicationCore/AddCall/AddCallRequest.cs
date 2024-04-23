using MediatR;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;

public record AddCallRequest(
    string CallerName,
    string Address,
    string Description
) : IRequest;