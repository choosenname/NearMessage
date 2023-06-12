using System;
using Client.Models;
using Client.Properties;
using Client.Stores;

namespace Client.Services;

public static class UserStoreSettingsService
{
    public static UserStore GetUserStore() =>
        new()
        {
            User = new UserModel(
                Guid.Empty,
                Settings.Default.Username,
                Settings.Default.Password),
            Token = Settings.Default.Token,
            LastResponseTime = Settings.Default.LastResponseTime
        };

    public static void SaveUserStore(UserStore userStore)
    {
        Settings.Default.Username = userStore.User.Username;
        Settings.Default.Password = userStore.User.Password;
        Settings.Default.Token = userStore.Token;
Settings.Default.LastResponseTime = userStore.LastResponseTime;
        Settings.Default.Save();
    }
}