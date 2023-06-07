using NearMessage.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMessage.Domain.Contacts;

public class Contact : Entity
{
    public Contact(Guid id, string username)
        : base(id)
    {
        Username = username;
    }

    public string Username { get; set; }
}
