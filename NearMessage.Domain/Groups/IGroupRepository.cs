using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.Groups;

public interface IGroupRepository
{
    Task<Result<Group>> CreateGroupAsync(Group group, CancellationToken cancellationToken);
}