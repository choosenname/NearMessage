using Client.Models;
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
    RegistrationViewModel _registrationViewModel;

    public RegistrationCommand(RegistrationViewModel registrationViewModel)
    {
        _registrationViewModel = registrationViewModel;

        _registrationViewModel.PropertyChanged += RegistrationViewModel_PropertyChanged;
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

        UserModel user = new UserModel(
            _registrationViewModel.Username,
            _registrationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(user),
            Encoding.UTF8, "application/json");

        var response = await App.HttpClient.PostAsync("/registration", content);

        response.EnsureSuccessStatusCode();

        _registrationViewModel.IsLoading = false;
    }
}
