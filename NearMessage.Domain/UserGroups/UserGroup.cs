using NearMessage.Domain.Groups;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.UserGroups;

public class UserGroup
{
    public UserGroup(Guid userId, Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }

    public Guid UserId { get; set; }

    public virtual User? User { get; set; }

    public Guid GroupId { get; set; }

    public virtual Group? Group { get; set; }
}