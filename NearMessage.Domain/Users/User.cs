using NearMessage.Domain.Chats;
using NearMessage.Domain.Primitives;

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

    public List<Chat> SentChats { get; set; } = new();

    public List<Chat> ReceivedChats { get; set; } = new();

    public bool VerifyPassword(string password) => password == Password;
}
