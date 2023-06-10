using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
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
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;

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
        {
            return new LastMessagesResponse(Result.Failure<IEnumerable<IEnumerable<Message>>>
                (new Error("User don't recognized")));
        }

        var maybeUser = await _userRepository.GetByIdAsync(maybeUserId.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return new LastMessagesResponse(Result.Failure<IEnumerable<IEnumerable<Message>>>
                (new Error("User doesn't exist")));
        }

        var receivedChats = maybeUser.Value.ReceivedChats;
        var messages = new List<IEnumerable<Message>>();

        foreach (var receivedChat in receivedChats)
        {
            var lastMessages = await _messageRepository.GetLastMessagesAsync(
                receivedChat.ChatId, request.LastResponseTime, cancellationToken);
        
            if(lastMessages.HasNoValue)
                continue;

            messages.Add(lastMessages.Value);
        }

        return new LastMessagesResponse(Result.Success<IEnumerable<IEnumerable<Message>>>(messages));
    }
}