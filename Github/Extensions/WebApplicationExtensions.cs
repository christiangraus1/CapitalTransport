using Github.Features.Github;

namespace Github.Extensions;


public static class WebApplicationExtensions
{
    public static WebApplication MapCustomEndpoints(this WebApplication app)
    {
        app.MapGithubEndpoints();

        return app;
    }
}
