using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Client.Commands;

public class AuthenticationCommand : CommandBase
{
    private readonly AuthenticationViewModel _authenticationViewModel;
    private readonly HttpClient _httpClient;
    private readonly NavigationStore _navigationStore;
    private UserStore _userStore;

    public AuthenticationCommand(AuthenticationViewModel authenticationViewModel,
        HttpClient httpClient, UserStore userStore, NavigationStore navigationStore)
    {
        _authenticationViewModel = authenticationViewModel;
        _httpClient = httpClient;
        _userStore = userStore;
        _navigationStore = navigationStore;

        authenticationViewModel.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthenticationViewModel.Username) ||
            e.PropertyName == nameof(AuthenticationViewModel.Password))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
        !string.IsNullOrEmpty(_authenticationViewModel.Username) &&
        !string.IsNullOrEmpty(_authenticationViewModel.Password);

    public async override void Execute(object? parameter)
    {
        _authenticationViewModel.IsLoading = true;

        _userStore = new UserStore(
            _authenticationViewModel.Username,
            _authenticationViewModel.Password);

        var content = new StringContent(JsonConvert.SerializeObject(_userStore.ToUserModel()),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/authentication", content);

        response.EnsureSuccessStatusCode();

        if(response.IsSuccessStatusCode)
        {
            _userStore.Token = await response.Content.ReadAsStringAsync();

            new NavigateCommand<ChatViewModel>(
            new NavigationService<ChatViewModel>(_navigationStore,
            () => new ChatViewModel(_userStore))).Execute(parameter);
        }

        _authenticationViewModel.IsLoading = false;
    }
}
