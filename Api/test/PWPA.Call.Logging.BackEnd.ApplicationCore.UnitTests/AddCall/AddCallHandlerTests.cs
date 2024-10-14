using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;
using PWPA.CallLogging.BackEnd.ApplicationCore.AddCall;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.UnitTests.AddCall;

public class AddCallHandlerTests
{
    private Mock<ICallsRepository> _repository = new();

    private Mock<IMessageProducer<AddCallRequest>> _sender = new();
    private AddCallHandler _sut;

    public AddCallHandlerTests()
    {
        _sut = new AddCallHandler(_repository.Object, _sender.Object);
    }

    [Fact]
    public async Task AddCall_Should_Invoke_Repository_AddCallAsync()
    {
        var request = new AddCallRequest("Test", "Test", "Test");
        await _sut.Handle(request, CancellationToken.None);

        _repository.Verify(r => r.AddCallAsync("Test", "Test", "Test"), Times.Once);
        _sender.Verify(s => s.Send(request), Times.Once);
    }
}