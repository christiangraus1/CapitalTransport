using AutoMapper;
using Github.BusinessLayer.Entities;

namespace Github.AutoMapper;
public class GithubMapping : Profile
{
    public GithubMapping()
    { 
        CreateMap<GithubUser, Features.Github.GetUsers.Result>()
            .ForMember(dest => dest.AverageNumberOfFollowers, opt => opt.MapFrom(e => e.NumberOfRepositories == 0 ? 0 : e.NumberOfFollowers/e.NumberOfRepositories));
    }
}