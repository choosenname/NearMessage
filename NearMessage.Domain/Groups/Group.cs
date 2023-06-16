using NearMessage.Domain.UserGroups;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Groups;

public class Group
{
    public Group(Guid id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual List<UserGroup>? UserGroups { get; set; }
}