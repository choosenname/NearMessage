using System;
using Client.Models;
using Client.Properties;
using Client.Stores;

namespace Client.Services;

public static class UserStoreSettingsService
{
    public static UserStore GetUserStore()
    {
        return new UserStore
        {
            User = new UserModel(
                Settings.Default.Id,
                Settings.Default.Username,
                Settings.Default.Password),
            Token = Settings.Default.Token,
            //Token = String.Empty,
            LastResponseTime = Settings.Default.LastResponseTime
        };
    }

    public static void SaveUserStore(UserStore userStore)
    {
        Settings.Default.Id = userStore.User.Id;
        Settings.Default.Username = userStore.User.Username;
        Settings.Default.Password = userStore.User.Password;
        Settings.Default.Token = userStore.Token;
        Settings.Default.LastResponseTime = userStore.LastResponseTime;
        Settings.Default.Save();
    }
}