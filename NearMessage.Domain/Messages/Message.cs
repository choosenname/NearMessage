using NearMessage.Domain.Primitives;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, Guid receiver)
        : base(id)
    {
        Content = content;
        Receiver = receiver;
    }

    public string Content { get; set; }

    public Guid Receiver { get; set; }
}
