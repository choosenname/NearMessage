using Client.Commands;
using Client.Services;
using Client.Stores;
using System;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class AuthenticationViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    private bool _isLoading = false;

    public string Username
    {
        get => _userStore.Username;
        set
        {
            _userStore.Username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _userStore.Password;
        set
        {
            _userStore.Password = value;
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

    public AuthenticationViewModel(HttpClient httpClient,
        UserStore userStore, NavigationStore navigationStore)
    {
        _userStore = userStore;

        AuthenticationCommand = new AuthenticationCommand(this, httpClient, userStore, navigationStore);

        NavigateCommand = new NavigateCommand<RegistrationViewModel>(
            new NavigationService<RegistrationViewModel>(navigationStore,
            () => new RegistrationViewModel(httpClient, userStore, navigationStore)));
    }
}