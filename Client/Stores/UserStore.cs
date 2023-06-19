using Client.Models;
using System;
using System.IO;
using Client.Properties;

namespace Client.Stores;

public class UserStore
{
    public UserModel User { get; set; } = new(Guid.Empty, string.Empty, string.Empty);

    public string Token { get; set; } = string.Empty;

    public DateTime LastResponseTime { get; set; }
}