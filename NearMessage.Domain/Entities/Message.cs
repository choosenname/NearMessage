namespace NearMessage.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }

    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}
