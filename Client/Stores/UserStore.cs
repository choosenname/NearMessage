using Client.Models;

namespace Client.Stores;

public class UserStore
{
    public UserModel User { get; set; }

    public string Token { get; set; }
}
