using Client.Stores;
using Client.ViewModels;

namespace Client.Commands;

public class SingInNavigateCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;

    public SingInNavigateCommand(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = new AuthenticationViewModel();
    }
}
