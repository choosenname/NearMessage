using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NearMessage.Application.Users.Commands.CreateUser;

namespace NearMessage.Presentation.Controllers;

public class RegistrationController : ApiControllerBase
{
    public RegistrationController(ISender sender) : base(sender) { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/registration", async (CancellationToken cancellationToken) =>
        {
            var comand = new CreateUserCommand(
                UserName: "Walfram",
                Password: "qwerty");

            var result = await Sender.Send(comand, cancellationToken);

            return result.IsSucceeded ? Results.Ok() : Results.BadRequest(result.Errors);
        });
    }
}
