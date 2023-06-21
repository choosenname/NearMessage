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
using System.Windows;
using Client.Interfaces;

namespace Client.Commands.Users;

public class RegistrationCommand : CommandBase
{
    private readonly HttpClient _httpClient;
    private readonly INavigationService _navigationService;
    private readonly RegistrationViewModel _registrationViewModel;
    private readonly UserStore _userStore;

    public RegistrationCommand(RegistrationViewModel registrationViewModel,
        HttpClient httpClient, UserStore userStore, INavigationService homeNavigationService)
    {
        _registrationViewModel = registrationViewModel;

        _registrationViewModel.PropertyChanged += OnPropertyChanged;
        _httpClient = httpClient;

        _userStore = userStore;
        _navigationService = homeNavigationService;
    }

    private void OnPropertyChanged(object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RegistrationViewModel.Username) ||
            e.PropertyName == nameof(RegistrationViewModel.Password))
            OnCanExecutedChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_registrationViewModel.Username) &&
               !string.IsNullOrEmpty(_registrationViewModel.Password);
    }


    public override async void Execute(object? parameter)
    {
        try
        {

            _registrationViewModel.IsLoading = true;

            _userStore.User = new UserModel(
                Guid.NewGuid(),
                _registrationViewModel.Username,
                _registrationViewModel.Password);

            var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/registration", content);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                _userStore.Token = await response.Content.ReadAsStringAsync();

                _userStore.Token = _userStore.Token.Trim('"');

                _navigationService.Navigate();
            }

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        finally
        {
            _registrationViewModel.IsLoading = false;
        }
    }
}