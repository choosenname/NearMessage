namespace NearMessage.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public bool IsActive { get => Connections.Count > 0; }
    public DateTime CreatedAt { get; set; }
    public List<Connection> Connections { get; set; }
}
