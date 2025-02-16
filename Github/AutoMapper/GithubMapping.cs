using AutoMapper;
using Github.BusinessLayer.Entities;

namespace Github.AutoMapper;
public class GithubMapping : Profile
{
    public GithubMapping()
    { 
        CreateMap<GithubUser, Features.Github.GetUsers.Result>();
    }
}