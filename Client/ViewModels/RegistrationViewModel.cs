using Client.Commands;
using Client.Models;
using Client.Services;
using Client.Stores;
using System;
using System.Net.Http;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Client.ViewModels;

public class RegistrationViewModel : ViewModelBase
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


    public ICommand RegistrationCommand { get; }
    public ICommand NavigationCommand { get; }

    public RegistrationViewModel(HttpClient httpClient, NavigationStore navigationStore)
    {
        RegistrationCommand = new RegistrationCommand(this, httpClient);

        NavigationCommand = new NavigationCommand<AuthenticationViewModel>(
            new NavigationService<AuthenticationViewModel>(navigationStore, () => new AuthenticationViewModel()));
    }
}
