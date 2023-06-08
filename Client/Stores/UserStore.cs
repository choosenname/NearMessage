using Client.Models;
using System;

namespace Client.Stores;

public class UserStore
{
    public UserModel User { get; set; } = new UserModel(Guid.Empty, String.Empty, String.Empty);

    public string Token { get; set; } = String.Empty;
}
