using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Messages.Commands.SaveMedia;

public sealed class SaveMediaCommandHandler : ICommandHandler<SaveMediaCommand, Result>
{
    public Task<Result> Handle(SaveMediaCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}