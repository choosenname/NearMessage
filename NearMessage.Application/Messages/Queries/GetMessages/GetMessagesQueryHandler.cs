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

    public GetMessagesQueryHandler(IMessageRepository messageRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<MessagesResponse> Handle(GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserIdAsync(request.Context.User);

        if (maybeSenderId.HasNoValue)
        {
            return new MessagesResponse(Result.Failure<List<Message>>(
                new("Can't find sender identifier")));
        }

        return new MessagesResponse(await _messageRepository.GetMessagesAsync(maybeSenderId.Value,
            request.Sender.Id, cancellationToken));
    }
}
