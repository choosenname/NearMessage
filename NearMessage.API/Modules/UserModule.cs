using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NearMessage.Application.Users.Queries.GetAllUsers;

namespace NearMessage.API.Modules;

public class UserModule : CarterModule
{
    public UserModule() :
        base("/users")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getall", [Authorize] async (ISender sender, HttpContext httpContext,
            CancellationToken cancellationToken) =>
        {
            var result = (await sender.Send(new GetAllUsersQuery(httpContext), cancellationToken)).Contacts;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });
    }
}
