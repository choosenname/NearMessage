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
            return new LastMessagesResponse(Result.Failure<IEnumerable<Media>>
                (new Error("User don't recognized")));

        var maybeUser = await _userRepository.GetByIdAsync(maybeUserId.Value, cancellationToken);

        if (maybeUser.HasNoValue)
            return new LastMessagesResponse(Result.Failure<IEnumerable<Media>>
                (new Error("User doesn't exist")));

        var receivedChats = maybeUser.Value.ReceivedChats;
        var messages = new List<Media>();

        if (receivedChats == null)
            return new LastMessagesResponse(Result.Success<IEnumerable<Media>>(messages));

        foreach (var receivedChat in receivedChats)
        {
            var lastMessages = await _messageRepository.GetLastMessagesAsync(
                receivedChat.ChatId, request.LastResponseTime, cancellationToken);

            if (lastMessages.HasNoValue)
                continue;

            messages.AddRange(lastMessages.Value);
        }

        var receivedGroups = maybeUser.Value.UserGroups;

        if (receivedGroups == null)
            return new LastMessagesResponse(Result.Success<IEnumerable<Media>>(messages));

        foreach (var receivedGroup in receivedGroups)
        {
            var lastMessages = await _messageRepository.GetLastMessagesAsync(
                receivedGroup.GroupId, request.LastResponseTime, cancellationToken);

            if (lastMessages.HasNoValue)
                continue;

            messages.AddRange(lastMessages.Value);
        }

        return new LastMessagesResponse(Result.Success<IEnumerable<Media>>(messages));
    }
}