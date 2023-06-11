using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NearMessage.Application.Users.Commands.UserRegistration;

namespace NearMessage.API.Modules;

public class RegistrationModule : CarterModule
{
    public RegistrationModule()
        : base("/registration")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async ([FromBody] UserRegistrationCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });
    }
}