using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Commands;

public class RegistrationCommand : CommandBase
{
    private readonly RegistrationViewModel _registrationViewModel;
    private readonly HttpClient _httpClient;
    private UserStore _userStore;
    private readonly NavigationService<ChatViewModel> _navigationService;

    public RegistrationCommand(RegistrationViewModel registrationViewModel,
        HttpClient httpClient, UserStore userStore, NavigationService<ChatViewModel> navigationService)
    {
        _registrationViewModel = registrationViewModel;

        _registrationViewModel.PropertyChanged += OnPropertyChanged;
        _httpClient = httpClient;

        _userStore = userStore;
        _navigationService = navigationService;
    }

    private void OnPropertyChanged(object? sender,
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

        _userStore.User = new UserModel(
            _registrationViewModel.Username,
            _registrationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.User),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/registration", content);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            _userStore.Token = await response.Content.ReadAsStringAsync();

            _navigationService.Navigate();
        }

        _registrationViewModel.IsLoading = false;
    }
}
