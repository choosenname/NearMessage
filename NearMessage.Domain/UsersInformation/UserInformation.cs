using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.UsersInformation;

public class UserInformation : Entity
{
    public UserInformation(Guid id, string? about, string name) : base(id)
    {
        About = about;
        Name = name;
    }

    public string? About { get; set; }

    public string Name { get; set; }
}