using AutoMapper;
using MediatR;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;

public class GetCallsHandler(ICallsRepository repository, IMapper mapper) : IRequestHandler<GetCallsRequest, IEnumerable<GetCallsResponse>>
{
    private readonly ICallsRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetCallsResponse>> Handle(GetCallsRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetCallsAsync();
        return _mapper.Map<IEnumerable<GetCallsResponse>>(result);
    }
}