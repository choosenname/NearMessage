using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.UserGroups;

public interface IUserGroupRepository
{
    Task<Result<UserGroup>> AddUserGroupAsync(UserGroup userGroup, CancellationToken cancellationToken);
}