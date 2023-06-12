using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Queries.GetMessages;

public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, MessagesResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public GetMessagesQueryHandler(IMessageRepository messageRepository,
        IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<MessagesResponse> Handle(GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var maybeReceiverId = _jwtProvider.GetUserId(request.HttpContext.User);

        if (maybeReceiverId.HasNoValue)
            return new MessagesResponse(Result.Failure<IEnumerable<Message>>(
                new Error("Can't find sender identifier")));

        return new MessagesResponse(await _messageRepository.GetMessagesAsync(
            maybeReceiverId.Value, cancellationToken));
    }
}