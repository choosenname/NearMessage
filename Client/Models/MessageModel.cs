using System;

namespace Client.Models;

public class MessageModel : EntityModel
{
    public MessageModel(Guid id, string content,
        Guid sender, Guid receiverChatId)
        : base(id)
    {
        Content = content;
        Sender = sender;
        ReceiverChatId = receiverChatId;
        SendTime = DateTime.Now;
    }

    public string Content { get; set; }

    public Guid Sender { get; set; }

    public Guid ReceiverChatId { get; set; }

    public DateTime SendTime { get; set; }
}