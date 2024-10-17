using MassTransit;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions;
using PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;

namespace PWPA.CallLogging.BackEnd.Infrastructure;

public class AddCallProducer(ISendEndpointProvider provider) : IMessageProducer<AddCallRequest>
{
    private readonly Uri Queue = new("queue:logged_calls_queue");

    public async Task SendAsync(AddCallRequest message)
    {
        ISendEndpoint endpoint = await provider.GetSendEndpoint(Queue);
        await endpoint.Send(message);
    }
}