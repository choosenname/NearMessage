using System;

namespace Client.Models;

public class UserModel
{
    public UserModel(Guid id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }
}
