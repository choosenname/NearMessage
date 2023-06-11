using NearMessage.Domain.Contacts;
using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, Guid sender, Guid receiverChatId)
        : base(id)
    {
        Content = content;
        SendTime = DateTime.Now;
        Sender = sender;
        ReceiverChatId = receiverChatId;
    }

    public string Content { get; set; }

    public DateTime SendTime { get; set; }

    public Guid Sender { get; set; }

    public Guid ReceiverChatId { get; set; }
}