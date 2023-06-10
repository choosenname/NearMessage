using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IJwtProvider _jwtProvider;

    public SaveMessageCommandHandler(IMessageRepository messageRepository,
        IChatRepository chatRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.Context.User);
        if (maybeSenderId.HasNoValue)
        {
            return Result.Failure(new("Can't find sender identifier"));
        }

        var maybeChat = await _chatRepository.GetChatAsync(maybeSenderId.Value,
            request.Message.ReceiverChat, cancellationToken);

        Chat chat;

        if (maybeChat.HasNoValue)
        {
            var chatResult = await _chatRepository.CreateChatAsync(maybeSenderId.Value,
                request.Message.ReceiverChat, cancellationToken);

            if (chatResult.IsFailure)
            {
                return Result.Failure(chatResult.Error);
            }

            chat = chatResult.Value;
        }
        else
        {
            chat = maybeChat.Value;
        }

        var result = await _messageRepository.SaveMessageAsync(
            chat,
            request.Message,
            cancellationToken);

        return result;
    }
}