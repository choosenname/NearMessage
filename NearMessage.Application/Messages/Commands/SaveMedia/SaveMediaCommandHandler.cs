using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMedia;

public sealed class SaveMediaCommandHandler : ICommandHandler<SaveMediaCommand, Result>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;

    public SaveMediaCommandHandler(IJwtProvider jwtProvider, IChatRepository chatRepository,
        IMessageRepository messageRepository)
    {
        _jwtProvider = jwtProvider;
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
    }

    public async Task<Result> Handle(SaveMediaCommand request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.Context.User);
        if (maybeSenderId.HasNoValue)
        {
            return Result.Failure(new Error("Can't find sender identifier"));
        }

        var maybeChat = await _chatRepository.GetChatAsync(maybeSenderId.Value,
            request.Media.ReceiverChat, cancellationToken);

        Chat chat;

        if (maybeChat.HasNoValue)
        {
            var chatResult = await _chatRepository.CreateChatAsync(maybeSenderId.Value,
                request.Media.ReceiverChat, cancellationToken);

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

        var result = await _messageRepository.SaveMediaAsync(
            chat,
            request.Media,
            cancellationToken);

        return result;
    }
}