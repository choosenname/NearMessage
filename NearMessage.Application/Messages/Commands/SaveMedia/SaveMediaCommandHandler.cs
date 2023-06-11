using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Application.Messages.Commands.SaveMedia;

public sealed class SaveMediaCommandHandler : ICommandHandler<SaveMediaCommand, Result>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public SaveMediaCommandHandler(IJwtProvider jwtProvider,
        IMessageRepository messageRepository)
    {
        _jwtProvider = jwtProvider;
        _messageRepository = messageRepository;
    }

    public async Task<Result> Handle(SaveMediaCommand request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.Context.User);
        if (maybeSenderId.HasNoValue)
        {
            return Result.Failure(new Error("Can't find sender identifier"));
        }

        var result = await _messageRepository.SaveMediaAsync(
            request.Media,
            cancellationToken);

        return result;
    }
}