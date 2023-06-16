using Client.Services;
using Client.Stores;

namespace Client.Commands.Users;

public class SaveUserStoreChangesCommand : CommandBase
{
    private readonly UserStore _userStore;

    public SaveUserStoreChangesCommand(UserStore userStore)
    {
        _userStore = userStore;
    }

    public override void Execute(object? parameter)
    {
        UserStoreSettingsService.SaveUserStore(_userStore);
    }
}