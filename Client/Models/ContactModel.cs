using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models;

public class ContactModel
{
    public ContactModel(Guid id, Guid chatID, string username)
    {
        Id = id;
        Username = username;
        CreatedAt = DateTime.Now;
        ChatID = chatID;
    }

    public Guid Id { get; set; }

    public string Username { get; set; }

    public Guid ChatID { get; set; }

    public DateTime CreatedAt { get; set; }
}
