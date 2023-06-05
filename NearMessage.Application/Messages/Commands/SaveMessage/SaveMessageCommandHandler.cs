using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;
using NearMessage.Domain.Users;
using System.Security.Claims;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed class SaveMessageCommandHandler : ICommandHandler<SaveMessageCommand, Result>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public SaveMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var claimUser = request.Context.User;
        var userIdClaim = claimUser.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Result.Failure(new("Can't find maybeUser identifier"));
        }

        var maybeUser = await _userRepository.GetByIdAsync(Guid.Parse(userIdClaim.Value), cancellationToken);

        if(maybeUser.HasNoValue)
        {
            return Result.Failure(new("The user with the specified id was not found."));
        }

        var result = await _messageRepository.SaveMessageAsync(
            maybeUser.Value,
            request.Message,
            cancellationToken);

        return result;
    }
}
