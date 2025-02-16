using AutoMapper;
using Github.BusinessLayer;
using Github.BusinessLayer.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Github.Features.Github.GetUsers;

public static partial class Endpoints
{
    public static RouteHandlerBuilder AddGithubGetUserEndpoint(this RouteGroupBuilder builder) =>
        builder
            .MapGet("/users", Endpoint.HandleAsync)
            .WithName(Endpoint.EndpointName);
}

public class Endpoint
{
    public const string EndpointName = "GithubGetUsers";

    public static async Task<Results<Ok<IEnumerable<Result>>, BadRequest<string>>> HandleAsync(
        HttpRequest Request,
        IGithubBusinessLayer github,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        try
        {
            var names = Request.Query["names"].ToString().Split(",");

            var result = await github.GetUsers(names.ToList());

            if(result != null)
                return TypedResults.Ok(result.Select(e => mapper.Map<Result>(e)));
        }
        catch(Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }

        return TypedResults.BadRequest("No data was found");
    }
}