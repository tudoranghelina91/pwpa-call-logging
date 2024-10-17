using MediatR;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;

public class AddCallHandler(ICallsRepository repository, IMessageProducer<AddCallRequest> sender) : IRequestHandler<AddCallRequest>
{
    private readonly ICallsRepository _repository = repository;

    public async Task Handle(AddCallRequest request, CancellationToken cancellationToken)
    {
        await _repository.AddCallAsync(request.CallerName, request.Address, request.Description);
        sender.SendAsync(request);
    }
}