namespace NearMessage.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Message Message { get; set; }
}
