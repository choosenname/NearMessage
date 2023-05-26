using Client.Commands;
using Client.Models;
using System;
using System.Windows.Input;

namespace Client.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    private string _username;
    private string _password;

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


    public ICommand RegistrationCommand { get; }

    public RegistrationViewModel()
    {
        RegistrationCommand = new RegistrationCommand(this);
    }
}
