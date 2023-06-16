using Client.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System;
using Client.Services;
using Client.ViewModels;
using Client.Interfaces;

namespace Client.Commands.Croup;

public class CreateGroupCommand : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly CreateGroupViewModel _createGroupViewModel;
    private readonly INavigationService _navigationService;


    public CreateGroupCommand(HttpClient httpClient, CreateGroupViewModel createGroupViewModel
    , INavigationService navigationService)
    {
        _httpClient = httpClient;
        _createGroupViewModel = createGroupViewModel;
        _navigationService = navigationService;
    }

    public override async void Execute(object? parameter)
    {
        if (_createGroupViewModel.GroupName != null)
            await GroupService.CreateGroupAsync(_createGroupViewModel.GroupName, _httpClient);

        _navigationService.Navigate();
    }
}