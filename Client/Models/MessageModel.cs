using System;

namespace Client.Models;

public class MessageModel
{
    public MessageModel(Guid id, string content, Guid sender, Guid receiverChatId)
    {
        Id = id;
        Content = content;
        Sender = sender;
        ReceiverChatId = receiverChatId;
        SendTime = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Content { get; set; }

    public Guid Sender { get; set; }

    public Guid ReceiverChatId { get; set; }

    public DateTime SendTime { get; set; }
}