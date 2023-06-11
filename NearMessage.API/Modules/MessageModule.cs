using Carter;
using MediatR;
using NearMessage.Application.Messages.Commands.SaveMessage;
using NearMessage.Application.Messages.Queries.GetLastMessages;
using NearMessage.Application.Messages.Queries.GetMessages;
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
            var result = (await sender.Send(new GetMessagesQuery(request, context),
                cancellationToken)).Messages;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        app.MapPost("/getlast", async (DateTime lastResponseTime, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = (await sender.Send(new GetLastMessagesQuery(lastResponseTime, context),
                cancellationToken)).Messages;

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        }).RequireAuthorization();

        app.MapPost("/send", async (Message request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context),
                cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        }).RequireAuthorization();

        app.MapPost("/sendfile", async (Media request, ISender sender,
            HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new SaveMessageCommand(request, context),
                cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        }).RequireAuthorization();
    }
}