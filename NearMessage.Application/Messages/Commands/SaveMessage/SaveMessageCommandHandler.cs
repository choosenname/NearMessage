using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message(
            Guid.NewGuid(),
            request.Content);

        var result = await _messageRepository.SaveMessageAsync(message, cancellationToken);

        return result;
    }
}
