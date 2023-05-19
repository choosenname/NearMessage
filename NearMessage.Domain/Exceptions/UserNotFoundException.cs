using NearMessage.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMessage.Domain.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userName) 
        : base($"User with user name {userName} was not found.")
    {
    }
    public UserNotFoundException(Guid Id) 
        : base($"User with id {Id} was not found.")
    {
    }
}
