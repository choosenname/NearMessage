using NearMessage.Domain.Primitives;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, 
        Guid receiverChat, DateTime sendTime)
        : base(id)
    {
        Content = content;
        ReceiverChat = receiverChat;
        SendTime = sendTime;
    }

    public string Content { get; set; }

    public Guid ReceiverChat { get; set; }

    public DateTime SendTime { get; set; }
}
