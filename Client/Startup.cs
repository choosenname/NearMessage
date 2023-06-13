using Client.Models;
using Client.Properties;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Client.Services;
using Client.Interfaces;

namespace Client;

public class Startup
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddSingleton<NavigationStore>();
        //services.AddSingleton<UserStore>();
        services.AddSingleton(UserStoreSettingsService.GetUserStore());

        services.AddSingleton(new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite),
            BaseAddress = new Uri(Settings.Default.HttpUriString)
        });

        services.AddSingleton<INavigationService>(CreateHomeNavigationService);

        services.AddSingleton<RegistrationViewModel>(s => new RegistrationViewModel(
            s.GetRequiredService<HttpClient>(),
            s.GetRequiredService<UserStore>(),
            CreateAuthenticationNavigationService(s),
            CreateHomeNavigationService(s)));

        services.AddSingleton<AuthenticationViewModel>(s => new AuthenticationViewModel(
            s.GetRequiredService<UserStore>(),
            s.GetRequiredService<HttpClient>(),
            CreateRegistrationNavigationService(s),
            CreateHomeNavigationService(s)));


        services.AddSingleton<HomeViewModel>(s => new HomeViewModel(
            s.GetRequiredService<UserStore>(),
            s.GetRequiredService<HttpClient>()));

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        return services.BuildServiceProvider();
    }

    private static INavigationService CreateAuthenticationNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<AuthenticationViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<AuthenticationViewModel>);
    }

    private static INavigationService CreateRegistrationNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<RegistrationViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<RegistrationViewModel>);
    }

    private static INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<HomeViewModel>);
    }
}