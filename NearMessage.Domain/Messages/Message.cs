using NearMessage.Domain.Primitives;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, 
        Guid receiver, DateTime sendTime)
        : base(id)
    {
        Content = content;
        Receiver = receiver;
        SendTime = sendTime;
    }

    public string Content { get; set; }

    public Guid Receiver { get; set; }

    public DateTime SendTime { get; set; }
}
