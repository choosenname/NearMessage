using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NearMessage.Application.Messages.Commands.SaveMessage;
using NearMessage.Application.Messages.Queries.GetMessages;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Messages;

namespace NearMessage.API.Modules;

public class MessageModule : CarterModule
{
    public MessageModule()
        : base("/message")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/get", async (Contact request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetMessagesQuery(request, context),
                cancellationToken);

            return result.Messages.IsSuccess ?
            Results.Ok(result.Messages.Value) :
            Results.BadRequest(result.Messages.Error);
        });

        app.MapPost("/send", [Authorize] async (Message request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context),
                cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });

        app.MapPost("/sendfile", [Authorize] async (Media request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context),
                cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}
