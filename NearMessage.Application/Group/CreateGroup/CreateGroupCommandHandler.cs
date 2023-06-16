using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Groups;
using NearMessage.Domain.Messages;
using NearMessage.Domain.UserGroups;

namespace NearMessage.Application.Group.CreateGroup;

public sealed record CreateGroupCommandHandler
    : ICommandHandler<CreateGroupCommand, Result<Domain.Groups.Group>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMessageRepository _messageRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository,
        IUserGroupRepository userGroupRepository, IJwtProvider jwtProvider, IMessageRepository messageRepository)
    {
        _groupRepository = groupRepository;
        _userGroupRepository = userGroupRepository;
        _jwtProvider = jwtProvider;
        _messageRepository = messageRepository;
    }

    public async Task<Result<Domain.Groups.Group>> Handle(CreateGroupCommand request,
        CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeUserId.HasNoValue)
            return Result.Failure<Domain.Groups.Group>(
                new Error("Can't find sender identifier"));

        var group = new Domain.Groups.Group(
            Guid.NewGuid(),
            request.Name + " • Group");

        var groupResult = await _groupRepository.CreateGroupAsync(group, cancellationToken);

        if (groupResult.IsFailure)
            return Result.Failure<Domain.Groups.Group>(groupResult.Error);

        var userGroup = new UserGroup(
            maybeUserId.Value,
            group.Id);

        var userGroupResult = await _userGroupRepository.AddUserAsync(userGroup, cancellationToken);

        if (userGroupResult.IsFailure)
            Result.Failure<Domain.Groups.Group>(userGroupResult.Error);

        var message = new Media(
            Guid.NewGuid(),
            $"Created new group {request.Name}",
            Guid.Empty,
            group.Id);

        var result = await _messageRepository.SaveMessageAsync(
            message,
            cancellationToken);

        if (result.IsFailure)
            Result.Failure<Domain.Groups.Group>(result.Error);

        return groupResult;
    }
}