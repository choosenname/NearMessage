using System;
using Client.Interfaces;
using Client.Properties;
using Client.Stores;

namespace Client.Commands;

public class LogOutCommand : CommandBase
{
    private readonly UserStore _userStore;
    private readonly INavigationService _authenticationNavigationService;

    public LogOutCommand(UserStore userStore, INavigationService authenticationNavigationService)
    {
        _userStore = userStore;
        _authenticationNavigationService = authenticationNavigationService;
    }

    public override void Execute(object? parameter)
    {
        Settings.Default.Token = String.Empty;
        _userStore.Token = String.Empty;
        _authenticationNavigationService.Navigate();
    }
}