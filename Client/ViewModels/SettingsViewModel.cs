using System.Threading;
using System.Windows.Input;
using Client.Commands;
using Client.Interfaces;
using Client.Models;
using Client.Queries;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private UserStore _userStore;
    private readonly UserInformationModel _informationModel;

    public SettingsViewModel(UserStore userStore, INavigationService homeNavigationService,
        INavigationService authenticationNavigationService)
    {
        _userStore = userStore;
        _informationModel = LoadEntityModelService.LoadEntity(_userStore.User.Id);
        
        ExitCommand = new ExitCommand(this, homeNavigationService);
        LogOutCommand = new LogOutCommand(userStore, authenticationNavigationService);
    }

    public ICommand ExitCommand { get; }
    public ICommand LogOutCommand { get; }

    public UserStore UserStore
    {
        get => _userStore;
        set
        {
            _userStore = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    public string? About
    {
        get => _informationModel.About;
        set
        {
            _informationModel.About = value;
            OnPropertyChanged(nameof(About));
        }
    }
}