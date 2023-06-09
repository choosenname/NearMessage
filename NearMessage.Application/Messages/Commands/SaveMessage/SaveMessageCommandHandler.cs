﻿using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository,
        IChatRepository chatRepository, IJwtProvider jwtProvider)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeSenderId.HasNoValue) return Result.Failure(new Error("Can't find sender identifier"));

        request.Media.Sender = maybeSenderId.Value;

        var result = await _messageRepository.SaveMessageAsync(
            request.Media,
            cancellationToken);

        return result;
    }
}