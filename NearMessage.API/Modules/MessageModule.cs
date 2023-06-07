using Carter;
using MediatR;
using NearMessage.Application.Messages.Queries.GetMessages;
using NearMessage.Domain.Contacts;

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
            HttpContext Context, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetMessagesQuery(request, Context),
                cancellationToken);

            return result.Messages.IsSuccess ?
            Results.Ok(result.Messages.Value) :
            Results.BadRequest(result.Messages.Error);
        });
    }
}
