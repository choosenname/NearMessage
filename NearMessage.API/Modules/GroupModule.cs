using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NearMessage.Application.Groups.Commands.CreateGroup;
using NearMessage.Application.UserGroups.Commands.CreateUserGroup;

namespace NearMessage.API.Modules;

public class GroupModule : CarterModule
{
    public GroupModule()
        : base("/group")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async ([FromBody] String name, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CreateGroupCommand(name, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        }).RequireAuthorization();

        app.MapPost("/join", async ([FromBody] Guid groupId, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CreateUserGroupCommand(groupId, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).RequireAuthorization();
    }
}