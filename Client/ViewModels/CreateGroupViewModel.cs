using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Croup;
using Client.Interfaces;

namespace Client.ViewModels;

public class CreateGroupViewModel : ViewModelBase
{
    private string? _groupName;

    public CreateGroupViewModel(HttpClient httpClient, INavigationService navigationService)
    {
        CreateGroupCommand = new CreateGroupCommand(httpClient, this, navigationService);
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
}