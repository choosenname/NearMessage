using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;

namespace NearMessage.Application.Chats.Commands.CreateChat;

public sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, Result<Chat>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IChatRepository _chatRepository;

    public CreateChatCommandHandler(IJwtProvider jwtProvider, IChatRepository chatRepository)
    {
        _jwtProvider = jwtProvider;
        _chatRepository = chatRepository;
    }

    public async Task<Result<Chat>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeUserId.HasNoValue) return Result.Failure<Chat>(new Error("Can't find sender identifier"));

        var maybeChat = await _chatRepository.CreateChatAsync(
            maybeUserId.Value, request.Contact.Id, cancellationToken);

        return maybeChat;
    }
}