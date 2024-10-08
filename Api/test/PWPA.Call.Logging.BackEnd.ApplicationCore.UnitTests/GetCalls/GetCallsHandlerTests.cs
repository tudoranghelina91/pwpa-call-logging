using AutoMapper;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;
using PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.UnitTests.GetCalls;

public class GetCallsHandlerTests
{
    private readonly Mock<ICallsRepository> _repository = new();
    private readonly MapperConfiguration _mapperConfig = new(cfg => cfg.AddProfile<GetCallsMappingProfile>());
    private readonly IMapper _mapper;
    private readonly GetCallsHandler _sut;

    public GetCallsHandlerTests()
    {
        _mapper = new Mapper(_mapperConfig);
        _sut = new GetCallsHandler(_repository.Object, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Return_GetCallsResponse_When_Called()
    {
        var request = new GetCallsRequest();
        var data = new List<Call>
        {
            new()
            {
                CallerName = nameof(Call.CallerName) + 1,
                Address = nameof(Call.Address) + 1,
                Description = nameof(Call.Description) + 1,
                Id = nameof(Call.Id) + 1
            },
            new()
            {
                CallerName = nameof(Call.CallerName) + 2,
                Address = nameof(Call.Address) + 2,
                Description = nameof(Call.Description) + 2,
                Id = nameof(Call.Id) + 2
            }
        };
        _repository.Setup(x => x.GetCallsAsync())
                   .ReturnsAsync(data);

        IEnumerable<GetCallsResponse> result = await _sut.Handle(request, CancellationToken.None);
        await Verify(result);
    }
}