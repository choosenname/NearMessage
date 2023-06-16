using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Groups;

public interface IGroupRepository
{
    Task<Result<Group>> CreateGroupAsync(Group group, CancellationToken cancellationToken);
    Task<IEnumerable<Group>> GetGroupsByUsernameAsync(string groupName, CancellationToken cancellationToken);
}