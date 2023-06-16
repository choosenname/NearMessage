using NearMessage.Domain.Chats;
using NearMessage.Domain.Groups;
using NearMessage.Domain.Primitives;
using NearMessage.Domain.UserGroups;

namespace NearMessage.Domain.Users;

public class User : Entity
{
    public User(Guid id, string username, string password)
        : base(id)
    {
        Username = username;
        Password = password;
        CreatedAt = DateTime.Now;
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual List<Chat>? SentChats { get; set; }

    public virtual List<Chat>? ReceivedChats { get; set; }

    public virtual List<UserGroup>? UserGroups { get; set; }

    public bool VerifyPassword(string password)
    {
        return password == Password;
    }
}