using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models;

public class UserModel
{
    public UserModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }

    public string Password { get; set; }
}
