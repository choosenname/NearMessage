using Client.Models;
using System;
using System.IO;
using System.Windows.Media.Media3D;
using Client.Properties;

namespace Client.Stores;

public class UserStore
{
    public UserModel User { get; set; } = new(Guid.Empty, string.Empty, string.Empty);

    public string Token { get; set; } = string.Empty;

    public string Theme { get; set; } = "Light";

    public DateTime LastResponseTime { get; set; }
}