using Client.Models;

namespace Client.Stores;

public class UserStore
{
    public UserStore(string username = "", string password = "")
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public UserModel ToUserModel() => new UserModel(Username, Password);
}
