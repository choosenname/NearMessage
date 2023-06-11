using NearMessage.Domain.Contacts;
using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Messages;

public class Message : Entity
{
    public Message(Guid id, string content, DateTime sendTime, Contact contact)
        : base(id)
    {
        Content = content;
        SendTime = sendTime;
        Contact = contact;
    }

    public string Content { get; set; }

    public DateTime SendTime { get; set; }

    public Contact Contact { get; set; }
}
