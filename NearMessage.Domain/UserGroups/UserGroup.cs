using NearMessage.Domain.Groups;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.UserGroups;

public class UserGroup
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid GroupId { get; set; }

    public Group Group { get; set; }
}