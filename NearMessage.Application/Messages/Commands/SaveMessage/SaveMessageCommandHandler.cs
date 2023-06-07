using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;
using NearMessage.Domain.Users;
using System.Security.Claims;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository, IChatRepository chatRepository)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
    }

    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var claimUser = request.Context.User;
        var userIdClaim = claimUser.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Result.Failure(new("Can't find maybeUser identifier"));
        }

        var chatResult = await _chatRepository.CreateChatAsync(Guid.Parse(userIdClaim.Value), 
            request.Message.Receiver, cancellationToken);

        if (chatResult.IsFailure)
        {
            return Result.Failure(chatResult.Error);
        }

        var result = await _messageRepository.SaveMessageAsync(
            chatResult.Value,
            request.Message,
            cancellationToken);

        return result;
    }
}
