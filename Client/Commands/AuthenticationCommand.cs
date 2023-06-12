using Client.Models;
using Client.Properties;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace Client.Commands;

public class AuthenticationCommand : CommandBase
{
    private readonly AuthenticationViewModel _authenticationViewModel;
    private readonly HttpClient _httpClient;
    private readonly NavigationService<HomeViewModel> _navigationService;
    private readonly UserStore _userStore;

    public AuthenticationCommand(AuthenticationViewModel authenticationViewModel,
        HttpClient httpClient, UserStore userStore, NavigationService<HomeViewModel> navigationService)
    {
        _authenticationViewModel = authenticationViewModel;
        _httpClient = httpClient;
        _userStore = userStore;
        _navigationService = navigationService;

        authenticationViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthenticationViewModel.Username) || 
            e.PropertyName == nameof(AuthenticationViewModel.Password))
            OnCanExecutedChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_authenticationViewModel.Username) &&
               !string.IsNullOrEmpty(_authenticationViewModel.Password);
    }

    public override async void Execute(object? parameter)
    {
        _authenticationViewModel.IsLoading = true;

        _userStore.User = new UserModel(
            Guid.Empty,
            _authenticationViewModel.Username,
            _authenticationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/authentication", content);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            _userStore.Token = await response.Content.ReadAsStringAsync();

            _userStore.Token = _userStore.Token.Trim('"');

            _navigationService.Navigate();
        }

        Settings.Default.Username = _userStore.User.Username;
        Settings.Default.Password = _userStore.User.Password;
        Settings.Default.Token = _userStore.Token;
        Settings.Default.Save();

        _authenticationViewModel.IsLoading = false;
    }
}