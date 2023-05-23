using Carter;
using MediatR;

namespace NearMessage.Presentation.Controllers;

public abstract class ApiControllerBase : CarterModule
{
    protected readonly ISender Sender;
    protected ApiControllerBase(ISender sender)
    {
        Sender = sender;
    }
}