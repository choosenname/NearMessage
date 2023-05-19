using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Entities;

public class User : Entity
{
    public User(Guid id, string username, string password, List<int> connections)
        : base(id)
    {
        Username = username;
        Password = password;
        CreatedAt = DateTime.Now;
        Connections = connections;
    }

    public string Username { get; set; }
    public string Password { get; set; }

    public bool IsActive { get => Connections.Count > 0; }
    public DateTime CreatedAt { get; set; }
    public List<int> Connections { get; set; }
}
