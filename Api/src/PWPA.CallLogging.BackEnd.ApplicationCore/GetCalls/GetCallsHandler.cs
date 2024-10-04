using AutoMapper;
using MediatR;
using PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions.Repositories;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;

public class GetCallsHandler(ICallsRepository repository, IMapper mapper) : IRequestHandler<GetCallsRequest, IEnumerable<GetCallsResponse>>
{
    private readonly ICallsRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetCallsResponse>> Handle(GetCallsRequest request, CancellationToken cancellationToken)
    {
        List<Call> result = await _repository.GetCallsAsync();
        return _mapper.Map<IEnumerable<GetCallsResponse>>(result);
    }
}