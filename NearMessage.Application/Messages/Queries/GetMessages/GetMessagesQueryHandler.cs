using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, MessagesResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IChatRepository _chatRepository;

    public GetMessagesQueryHandler(IMessageRepository messageRepository,
        IJwtProvider jwtProvider, IChatRepository chatRepository)
    {
        _messageRepository = messageRepository;
        _jwtProvider = jwtProvider;
        _chatRepository = chatRepository;
    }

    public async Task<MessagesResponse> Handle(GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var maybeReceiverId = _jwtProvider.GetUserId(request.Context.User);

        if (maybeReceiverId.HasNoValue)
        {
            return new MessagesResponse(Result.Failure<IEnumerable<Message>>(
                new("Can't find sender identifier")));
        }

        var maybeChat = await _chatRepository.GetChatAsync(request.Sender.ChatId, 
            maybeReceiverId.Value, cancellationToken);

        Chat chat;

        if (maybeChat.HasNoValue)
        {
            var chatResult = await _chatRepository.CreateChatAsync(request.Sender.ChatId,
                maybeReceiverId.Value, cancellationToken);

            if (chatResult.IsFailure)
            {
                return new MessagesResponse(Result.Failure<IEnumerable<Message>>(chatResult.Error));
            }

            chat = chatResult.Value;
        }
        else
        {
            chat = maybeChat.Value;
        }

        return new MessagesResponse(await _messageRepository.GetMessagesAsync(chat.ChatId, cancellationToken));
    }
}
