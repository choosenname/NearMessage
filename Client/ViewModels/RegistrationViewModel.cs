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

    public ICommand RegistrationCommand { get; }
    public ICommand NavigateCommand { get; }

    public RegistrationViewModel(HttpClient httpClient, UserStore userStore, NavigationStore navigationStore)
    {
        _userStore = userStore;

        RegistrationCommand = new RegistrationCommand(this, httpClient, userStore);

        NavigateCommand = new NavigateCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(navigationStore, 
            () => new AuthenticationViewModel(httpClient, userStore, navigationStore)));
    }
}
