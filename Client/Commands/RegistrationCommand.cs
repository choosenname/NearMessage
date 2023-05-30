using Client.Models;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Client.Commands;

public class RegistrationCommand : CommandBase
{
    private readonly RegistrationViewModel _registrationViewModel;
    private readonly HttpClient _httpClient;
    private UserStore _userStore;

    public RegistrationCommand(RegistrationViewModel registrationViewModel, 
        HttpClient httpClient, UserStore userStore)
    {
        _registrationViewModel = registrationViewModel;

        _registrationViewModel.PropertyChanged += RegistrationViewModel_PropertyChanged;
        _httpClient = httpClient;

        _userStore = userStore;
    }

    private void RegistrationViewModel_PropertyChanged(object? sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RegistrationViewModel.Username) ||
            e.PropertyName == nameof(RegistrationViewModel.Password))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_registrationViewModel.Username) &&
        !string.IsNullOrEmpty(_registrationViewModel.Password);


    public async override void Execute(object? parameter)
    {
        _registrationViewModel.IsLoading = true;

        _userStore = new UserStore(
            _registrationViewModel.Username,
            _registrationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.ToUserModel()),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/registration", content);

        response.EnsureSuccessStatusCode();

        _registrationViewModel.IsLoading = false;
    }
}
