using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Entities;

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
    //public List<int> Connections { get; set; }

    public bool VerifyPassword(string password) => password == Password;
}
