using Client.Properties;
using System;
using System.IO;

namespace Client.Models;

public class UserModel : EntityModel
{
    public UserModel(Guid id, string username, string password)
        : base(id)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }

    public string Avatar => Path.Combine(Settings.Default.UserPhotoPath, Id.ToString().ToUpper() + ".png");
}