using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.UserGroups;

namespace NearMessage.Application.UserGroups.Commands.CreateUserGroup;

public sealed class CreateUserGroupCommandHandler
: ICommandHandler<CreateUserGroupCommand, Result<Guid>>
{
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IJwtProvider _jwtProvider;

    public CreateUserGroupCommandHandler(IUserGroupRepository userGroupRepository, IJwtProvider jwtProvider)
    {
        _userGroupRepository = userGroupRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<Guid>> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeUserId.HasNoValue)
            return Result.Failure<Guid>(
                new Error("Can't find sender identifier"));

        var userGroup = new UserGroup(
            maybeUserId.Value,
            request.GroupId);

        var userGroupResult = await _userGroupRepository.AddUserGroupAsync(userGroup, cancellationToken);

        if (userGroupResult.IsFailure)
            Result.Failure<UserGroup>(userGroupResult.Error);

        return Result.Success(userGroupResult.Value.GroupId);
    }
}