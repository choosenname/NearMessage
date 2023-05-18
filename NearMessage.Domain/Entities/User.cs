using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Entities;

public class User : Entity
{
    public User(Guid id, string username, string email, DateTime createdAt, List<int> connections)
        : base(id)
    {
        Username = username;
        CreatedAt = createdAt;
        Connections = connections;
    }

    public string Username { get; set; }

    public bool IsActive { get => Connections.Count > 0; }
    public DateTime CreatedAt { get; set; }
    public List<int> Connections { get; set; }
}
