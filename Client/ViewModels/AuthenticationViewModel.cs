using Client.Commands;
using Client.Services;
using Client.Stores;
using System.Net.Http;
using System.Windows.Input;
using Client.Commands.Navigation;
using Client.Commands.Users;
using Client.Interfaces;

namespace Client.ViewModels;

public class AuthenticationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    private bool _isLoading;

    public AuthenticationViewModel(UserStore userStore, HttpClient httpClient,
        INavigationService registrationNavigationService, INavigationService homeNavigationService)
    {
        _userStore = userStore;

        NavigateCommand = new NavigateCommand(registrationNavigationService);

        AuthenticationCommand = new AuthenticationCommand(
            this, httpClient, userStore, homeNavigationService);
    }

    public string Username
    {
        get => _userStore.User.Username;
        set
        {
            _userStore.User.Username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _userStore.User.Password;
        set
        {
            _userStore.User.Password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public ICommand NavigateCommand { get; }
    public ICommand AuthenticationCommand { get; }
}