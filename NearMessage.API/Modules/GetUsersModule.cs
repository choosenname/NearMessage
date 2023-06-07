using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NearMessage.Application.Users.Queries.GetAllUsers;

namespace NearMessage.API.Modules;

public class GetUsersModule : CarterModule
{
    public GetUsersModule() :
        base("users/getall")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", [Authorize] async (ISender sender, CancellationToken cancellationToken) =>
            (await sender.Send(new GetAllUsersQuery(), cancellationToken)).Сontacts
        );
    }
}
