using System;

namespace Client.Models;

public class ContactModel : EntityModel
{
    public ContactModel(Guid id, string username, Guid? chatId)
    :base (id)
    {
        Username = username;
        ChatId = chatId;
    }

    public string Username { get; set; }

    public Guid? ChatId { get; set; }
}