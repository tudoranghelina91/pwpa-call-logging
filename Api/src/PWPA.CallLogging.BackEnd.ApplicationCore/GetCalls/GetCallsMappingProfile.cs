using AutoMapper;
using PWPA.CallLogging.BackEnd.ApplicationCore.Models;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.GetCalls;

public class GetCallsMappingProfile : Profile
{
    public GetCallsMappingProfile()
    {
        CreateMap<Call, GetCallsResponse>()
            .ForMember(dst => dst.CallerName, opt => opt.MapFrom(src => src.CallerName))
            .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description));
    }
}