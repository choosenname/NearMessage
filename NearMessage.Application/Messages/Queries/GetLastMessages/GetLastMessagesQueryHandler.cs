using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Messages.Queries.GetLastMessages;

public sealed class GetLastMessagesQueryHandler : IQueryHandler<GetLastMessagesQuery, LastMessagesResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public GetLastMessagesQueryHandler(IJwtProvider jwtProvider,
        IUserRepository userRepository, IMessageRepository messageRepository)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }

    public async Task<LastMessagesResponse> Handle(GetLastMessagesQuery request, CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);

        if (maybeUserId.HasNoValue)
            return new LastMessagesResponse(Result.Failure<IDictionary<Guid, IEnumerable<Message>>>
                (new Error("User don't recognized")));

        var maybeUser = await _userRepository.GetByIdAsync(maybeUserId.Value, cancellationToken);

        if (maybeUser.HasNoValue)
            return new LastMessagesResponse(Result.Failure<IDictionary<Guid, IEnumerable<Message>>>
                (new Error("User doesn't exist")));

        var receivedChats = maybeUser.Value.ReceivedChats;
        var messages = new Dictionary<Guid, IEnumerable<Message>>();

        if (receivedChats == null)
            return new LastMessagesResponse(Result.Success<IDictionary<Guid, IEnumerable<Message>>>(messages));

        foreach (var receivedChat in receivedChats)
        {
            var lastMessages = await _messageRepository.GetLastMessagesAsync(
                receivedChat.ChatId, request.LastResponseTime, cancellationToken);

            if (lastMessages.HasNoValue)
                continue;

            messages.Add(receivedChat.ChatId, lastMessages.Value);
        }

        return new LastMessagesResponse(Result.Success<IDictionary<Guid, IEnumerable<Message>>>(messages));
    }
}