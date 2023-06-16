using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.UserGroups;

public interface IUserGroupRepository
{
    Task<Result<UserGroup>> AddUserAsync(UserGroup userGroup, CancellationToken cancellationToken);
}