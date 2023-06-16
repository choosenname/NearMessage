using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NearMessage.Application.Group.CreateGroup;

namespace NearMessage.API.Modules;

public class GroupModule : CarterModule
{
    public GroupModule()
        : base("/group")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async ([FromBody]String name, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CreateGroupCommand(name, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).RequireAuthorization();
    }
}