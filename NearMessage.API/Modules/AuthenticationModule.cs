using Carter;
using MediatR;
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
        app.MapPost("", async (UserAuthenticationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            Result<string> result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });
    }
}
