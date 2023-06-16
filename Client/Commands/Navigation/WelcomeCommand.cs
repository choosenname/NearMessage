using Client.Interfaces;
using Client.Stores;
using Client.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client.Commands.Navigation;

public class WelcomeCommand : CommandBase
{
    private readonly INavigationService _homeNavigationService;
    private readonly INavigationService _registrationNavigationService;
    private readonly INavigationService _authenticationNavigationService;
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;

    public WelcomeCommand(HttpClient httpClient, UserStore userStore, INavigationService homeNavigationService,
        INavigationService registrationNavigationService, INavigationService authenticationNavigationService)
    {
        _httpClient = httpClient;
        _userStore = userStore;
        _homeNavigationService = homeNavigationService;
        _registrationNavigationService = registrationNavigationService;
        _authenticationNavigationService = authenticationNavigationService;
    }

    public override async void Execute(object? parameter)
    {
        if (string.IsNullOrEmpty(_userStore.Token))
        {
            _registrationNavigationService.Navigate();
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                _userStore.Token);

            var response = await _httpClient.PostAsync("/authentication/confirm", null);

            if (!response.IsSuccessStatusCode)
                _authenticationNavigationService.Navigate();
            else
                _homeNavigationService.Navigate();
        }
    }
}