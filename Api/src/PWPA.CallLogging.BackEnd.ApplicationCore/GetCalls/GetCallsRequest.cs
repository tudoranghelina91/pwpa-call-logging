using MediatR;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;

public record GetCallsRequest : IRequest<IEnumerable<GetCallsResponse>>;