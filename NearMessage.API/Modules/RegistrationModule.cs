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
        app.MapPost("", async (ISender sender) =>
        {
            var comand = new CreateUserCommand(
                UserName: "Walfram",
                Password: "qwerty");

            await sender.Send(comand);

            return Results.Ok();
        });
    }
}
