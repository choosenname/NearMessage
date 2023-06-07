using System;

namespace Client.Models;

public class MessageModel
{
    public MessageModel(Guid id, string content, ContactModel receiver)
    {
        Id = id;
        Content = content;
        Receiver = receiver;
    }

    public Guid Id { get; set; }

    public string Content { get; set; }

    public ContactModel Receiver { get; set; }
}
