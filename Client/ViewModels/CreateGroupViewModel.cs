using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Group;
using Client.Commands.Navigation;
using Client.Interfaces;

namespace Client.ViewModels;

public class CreateGroupViewModel : ViewModelBase
{
    private string? _groupName;

    public CreateGroupViewModel(HttpClient httpClient, INavigationService navigationService, INavigationService closeNavigationService)
    {
        CreateGroupCommand = new CreateGroupCommand(httpClient, this, navigationService);
        CloseModalWindowCommand = new NavigateCommand(closeNavigationService);
    }

    public string? GroupName
    {
        get => _groupName;
        set
        {
            _groupName = value;
            OnPropertyChanged(nameof(GroupName));
        }
    }

    public ICommand CreateGroupCommand { get; }
    public ICommand CloseModalWindowCommand { get; }
}