using MediatR;
using Microsoft.AspNetCore.Mvc;
using NearMessage.Application.Users.Commands.CreateUser;
using Server.Controllers;

namespace NearMessage.API.Controllers;

[Route("api/registration")]
[ApiController]
public class RegistrationController : ApiController
{
    public RegistrationController(ISender sender) : base(sender) { }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(CancellationToken cancellationToken)
    {
        var comand = new CreateUserAsyncCommand(
            UserName: "Walfram",
            Password: "qwerty");

        var result =  await Sender.Send(comand, cancellationToken);

        return result.IsSucceeded ? Ok() : BadRequest(result.Errors);
    }
}
