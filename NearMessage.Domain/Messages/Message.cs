using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content)
        : base(id)
    {
        Content = content;
    }

    public string Content { get; set; }
}
