using Carter;
using MediatR;
using NearMessage.Application.Users.Commands.CreateUser;

namespace NearMessage.Presentation.Controllers;

public static class RegistrationController 
{
    public static void AddRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("/registration", async (ISender sender) =>
        {
            var comand = new CreateUserCommand(
                UserName: "Walfram",
                Password: "qwerty");

            await sender.Send(comand);

            return Results.Ok();
        });
    }   
}
