using Carter;
using MediatR;
using NearMessage.Application.Users.Commands.CreateUser;

namespace NearMessage.API.Modules;

public class RegistrationModule : CarterModule
{
    public RegistrationModule()
        :base("/registration")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var comand = new UserRegistrationCommand(
                UserName: "Walfram",
                Password: "qwerty");

            var result = await sender.Send(comand, cancellationToken);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Error);
        });
    }
}
