using Client.Commands;
using Client.Services;
using Client.Stores;
using System;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class AuthenticationViewModel : ViewModelBase
{
    private string _username = String.Empty;
    private string _password = String.Empty;
    private bool _isLoading = false;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
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

    public AuthenticationViewModel(HttpClient httpClient, NavigationStore navigationStore)
    {

        NavigateCommand = new NavigateCommand<RegistrationViewModel>(
            new NavigationService<RegistrationViewModel>(navigationStore,
            () => new RegistrationViewModel(httpClient, navigationStore)));
    }
}