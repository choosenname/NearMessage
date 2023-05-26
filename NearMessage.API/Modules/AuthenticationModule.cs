using Carter;
using MediatR;
using NearMessage.Application.Users.Commands.UserAuthentication;

namespace NearMessage.API.Modules;

public class AuthenticationModule : CarterModule
{
    public AuthenticationModule()
        : base("/registration")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async (UserAuthenticationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}
