using Client.Properties;
using System;
using System.IO;

namespace Client.Models;

public class ContactModel : EntityModel
{
    public ContactModel(Guid id, string username, Guid? chatId)
        : base(id)
    {
        Username = username;
        ChatId = chatId;
    }

    public string Username { get; set; }

    public Guid? ChatId { get; set; }

    public string Avatar => Path.Combine(Settings.Default.UserPhotoPath, Id.ToString().ToUpper() + ".png");

}