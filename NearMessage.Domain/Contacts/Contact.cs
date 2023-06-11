using NearMessage.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMessage.Domain.Contacts;

public class Contact : Entity
{
    public Contact(Guid id, string username, Guid receiver)
        : base(id)
    {
        Username = username;
        Receiver = receiver;
    }

    public string Username { get; set; }

    public Guid Receiver { get; set; }
}
