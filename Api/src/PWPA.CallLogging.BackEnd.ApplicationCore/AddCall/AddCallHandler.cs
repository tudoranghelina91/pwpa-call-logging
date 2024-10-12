using MediatR;
using MassTransit;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;

public class AddCallHandler(ICallsRepository repository, ISendEndpointProvider sender) : IRequestHandler<AddCallRequest>
{
    private readonly ICallsRepository _repository = repository;

    public async Task Handle(AddCallRequest request, CancellationToken cancellationToken)
    {
        await _repository.AddCallAsync(request.CallerName, request.Address, request.Description);
        var endpoint = await sender.GetSendEndpoint(new Uri("queue:logged_calls_queue"));
        await endpoint.Send(request);
    }
}