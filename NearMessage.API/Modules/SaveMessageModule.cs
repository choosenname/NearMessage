using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NearMessage.Application.Messages.Commands.SaveMessage;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.API.Modules;

public class SaveMessageModule : CarterModule
{
    public SaveMessageModule()
        : base("/receiver")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", [Authorize] async (Message request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new SaveMessageCommand(request, context), cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}
