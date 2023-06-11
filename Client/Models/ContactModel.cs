using System;

namespace Client.Models;

public class ContactModel
{
    public ContactModel(Guid id, string username)
    {
        Id = id;
        Username = username;
    }

    public Guid Id { get; set; }

    public string Username { get; set; }

    public Guid? ChatId { get; set; }
}