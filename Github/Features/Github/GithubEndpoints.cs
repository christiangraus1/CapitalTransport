using Github.Features.Github.GetUsers;

namespace Github.Features.Github;

public static class GithubEndpoints
{
    public static WebApplication MapGithubEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/github")
            .WithTags("GitHub");

        group.AddGithubGetUserEndpoint();

        return app;
    }
}
