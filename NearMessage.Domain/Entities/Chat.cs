namespace NearMessage.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<User> Participants { get; set; }
    public List<Message> Messages { get; set; }

    public DateTime CreatedAt { get; set; }
}
