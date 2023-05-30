using Client.Models;

namespace Client.Stores;

public class UserStore
{
    public UserStore(string username = "", string password = "", string token = "")
    {
        Username = username;
        Password = password;
        Token = token;
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Token { get; set; }

    public UserModel ToUserModel() => new UserModel(Username, Password);
}
