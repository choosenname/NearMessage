using Client.Commands;
using Client.Stores;
using System.Windows.Input;

namespace Client.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    public ICommand NavigateCommand { get; }

    public RegistrationViewModel(NavigationStore navigationStore)
    {
        NavigateCommand = new NavigateCommand(navigationStore);
    }
}
