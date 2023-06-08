using NearMessage.Domain.Chats;
using NearMessage.Domain.Contacts;
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
        SentChats = new List<Chat>();
        ReceivedChats = new List<Chat>();
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual List<Chat>? SentChats { get; set; }

    public virtual List<Chat>? ReceivedChats { get; set; }

    public bool VerifyPassword(string password) => password == Password;
}
