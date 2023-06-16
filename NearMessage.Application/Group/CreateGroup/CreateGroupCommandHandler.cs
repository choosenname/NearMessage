using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Groups;
using NearMessage.Domain.UserGroups;

namespace NearMessage.Application.Group.CreateGroup;

public sealed record CreateGroupCommandHandler
: ICommandHandler<CreateGroupCommand, Result<Domain.Groups.Group>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IJwtProvider _jwtProvider;

    public CreateGroupCommandHandler(IGroupRepository groupRepository,
        IUserGroupRepository userGroupRepository, IJwtProvider jwtProvider)
    {
        _groupRepository = groupRepository;
        _userGroupRepository = userGroupRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<Domain.Groups.Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeUserId.HasNoValue) return Result.Failure<Domain.Groups.Group>(
            new Error("Can't find sender identifier"));

        var group = new Domain.Groups.Group(
            Guid.NewGuid(),
            request.Name);

        var groupResult = await _groupRepository.CreateGroupAsync(group, cancellationToken);

        if (groupResult.IsFailure)
            return Result.Failure<Domain.Groups.Group>(groupResult.Error);

        var userGroup = new UserGroup(
            maybeUserId.Value,
            group.Id);

        var userGroupResult = await _userGroupRepository.AddUserAsync(userGroup, cancellationToken);

        return userGroupResult.IsFailure 
            ? Result.Failure<Domain.Groups.Group>(userGroupResult.Error) 
            : groupResult;
    }
}