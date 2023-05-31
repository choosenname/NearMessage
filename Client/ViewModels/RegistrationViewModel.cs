using Client.Commands;
using Client.Services;
using Client.Stores;
using System;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    private bool _isLoading = false;

    public string Username
    {
        get => _userStore.User?.Username;
        set
        {
            _userStore.User.Username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _userStore.User?.Password;
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
    public ICommand RegistrationCommand { get; }

    public RegistrationViewModel(HttpClient httpClient, UserStore userStore, NavigationStore navigationStore)
    {
        _userStore = userStore;

        NavigateCommand = new NavigateCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(navigationStore,
            () => new AuthenticationViewModel(httpClient, userStore, navigationStore)));

        var navigateService = new NavigationService<ChatViewModel>(
            navigationStore,
            () => new ChatViewModel(userStore));

        RegistrationCommand = new RegistrationCommand(this, httpClient, userStore, navigateService);
    }
}
