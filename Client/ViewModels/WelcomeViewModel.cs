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
    public WelcomeViewModel(HttpClient httpClient, UserStore userStore, INavigationService homeNavigationService,
        INavigationService registrationNavigationService, INavigationService authenticationNavigationService)
    {
        WelcomeCommand = new WelcomeCommand(httpClient, userStore, homeNavigationService, registrationNavigationService,
            authenticationNavigationService);
    }

    public ICommand WelcomeCommand { get; }
}