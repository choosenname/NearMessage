using Carter;
using MediatR;
using NearMessage.Application.Messages.Commands.SaveMessage;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.API.Modules;

public class SaveMessageModule : CarterModule
{
    public SaveMessageModule()
        : base("/receiver")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", async (SaveMessageCommand request,
            ISender sender, CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(request, cancellationToken);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}
