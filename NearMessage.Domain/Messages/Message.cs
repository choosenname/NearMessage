using NearMessage.Domain.Entities;
using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, User receiver)
        : base(id)
    {
        Content = content;
        Receiver = receiver;
    }

    public string Content { get; set; }

    public User Receiver { get; set; }
}
