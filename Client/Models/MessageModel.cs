using System;

namespace Client.Models;

public class MessageModel
{
    public MessageModel(Guid id, string content, Guid receiver)
    {
        Id = id;
        Content = content;
        Receiver = receiver;
        SendTime = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Content { get; set; }

    public Guid Sender { get; set; }

    public Guid Receiver { get; set; }

    public DateTime SendTime { get; set; }
}