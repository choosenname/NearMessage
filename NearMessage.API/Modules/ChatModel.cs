using Carter;
using MediatR;
using NearMessage.Application.Chats.Commands.CreateChat;
using NearMessage.Domain.Contacts;

namespace NearMessage.API.Modules;

public class ChatModel : CarterModule
{
    public ChatModel()
        : base("/chat")
    {
    }


    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (Contact request, HttpContext httpContext,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new CreateChatCommand(request, httpContext), cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value.ChatId) : Results.BadRequest(result.Error);
        }).RequireAuthorization();
    }
}