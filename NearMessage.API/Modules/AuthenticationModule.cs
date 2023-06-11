using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NearMessage.Application.Users.Commands.UserAuthentication;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.API.Modules;

public class AuthenticationModule : CarterModule
{
    public AuthenticationModule()
        : base("/authentication")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async ([FromBody] UserAuthenticationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        app.MapPost("/confirm", () => { }).RequireAuthorization();
    }
}
