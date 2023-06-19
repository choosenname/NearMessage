using System.Net.Http;
using System.Threading;
using System.Windows.Input;
using Client.Commands;
using Client.Commands.Navigation;
using Client.Interfaces;
using Client.Stores;

namespace Client.ViewModels;

public class WelcomeViewModel : ViewModelBase
{
    private bool _isLoading = false;

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public WelcomeViewModel(HttpClient httpClient, UserStore userStore, INavigationService homeNavigationService,
        INavigationService registrationNavigationService, INavigationService authenticationNavigationService)
    {
        WelcomeCommand = new WelcomeCommand(httpClient, userStore, homeNavigationService, registrationNavigationService,
            authenticationNavigationService, this);
    }

    public ICommand WelcomeCommand { get; }
}