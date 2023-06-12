using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Chats.Commands.GetChat;

public sealed class GetChatCommandHandler : ICommandHandler<GetChatCommand, Result<Chat>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IChatRepository _chatRepository;

    public GetChatCommandHandler(IChatRepository chatRepository, IJwtProvider jwtProvider)
    {
        _chatRepository = chatRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<Chat>> Handle(GetChatCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeUserId.HasNoValue) return Result.Failure<Chat>(new Error("Can't find sender identifier"));

        var maybeChat = await _chatRepository.GetChatByUsersIdAsync(
            maybeUserId.Value, request.Contact.Id, cancellationToken);

        if (maybeChat.HasNoValue) return Result.Failure<Chat>(new Error("Can't find chat"));

        return Result.Success(maybeChat.Value);
    }
}