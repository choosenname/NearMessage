using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Entities;

public class Message : Entity
{
    public Message(Guid id, string content, DateTime sentAt, bool isRead, bool isDeleted)
        : base(id)
    {
        Content = content;
        SentAt = sentAt;
        IsRead = isRead;
        IsDeleted = isDeleted;
    }

    public string Content { get; set; }
    public DateTime SentAt { get; set; }

    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}
