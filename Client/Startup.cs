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
        services.AddSingleton<ModalNavigationStore>();
        //services.AddSingleton<UserStore>();
        services.AddSingleton<UserStore>(UserStoreSettingsService.GetUserStore());

        services.AddSingleton<HttpClient>(new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite),
            BaseAddress = new Uri(Settings.Default.HttpUriString)
        });

        services.AddSingleton<INavigationService>(CreateWelcomeNavigationService);
        services.AddSingleton<CloseModalNavigationService>();

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
            s.GetRequiredService<HttpClient>(),
            CreateSettingsNavigationService(s),
            CreateCreateGroupNavigationService(s),
            CreateAuthenticationNavigationService(s)));

        services.AddTransient<SettingsViewModel>(CreateSettingsViewModel);

        services.AddTransient<WelcomeViewModel>(s => new WelcomeViewModel(
            s.GetRequiredService<HttpClient>(),
            s.GetRequiredService<UserStore>(),
            CreateHomeNavigationService(s),
            CreateRegistrationNavigationService(s),
            CreateAuthenticationNavigationService(s)));

        services.AddTransient<CreateGroupViewModel>(CreateCreateGroupViewModel);

        services.AddSingleton<MainViewModel>(s => new MainViewModel(
            s.GetRequiredService<NavigationStore>(),
            s.GetRequiredService<ModalNavigationStore>(),
            s.GetRequiredService<UserStore>()));

        services.AddSingleton<MainWindow>(provider => new MainWindow(
            provider.GetRequiredService<MainViewModel>()));

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

    private static INavigationService CreateSettingsNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<SettingsViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<SettingsViewModel>);
    }

    private static INavigationService CreateWelcomeNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<WelcomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<WelcomeViewModel>);
    }

    private static INavigationService CreateCreateGroupNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<CreateGroupViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<CreateGroupViewModel>);
    }

    private static SettingsViewModel CreateSettingsViewModel(IServiceProvider serviceProvider)
    {
        var navigationService1 = new CompositeNavigationService(
            serviceProvider.GetRequiredService<CloseModalNavigationService>(),
            CreateHomeNavigationService(serviceProvider));

        return new SettingsViewModel(
            serviceProvider.GetRequiredService<UserStore>(),
            navigationService1);
    }

    private static CreateGroupViewModel CreateCreateGroupViewModel(IServiceProvider serviceProvider)
    {
        var navigationService = new CompositeNavigationService(
            serviceProvider.GetRequiredService<CloseModalNavigationService>(),
            CreateHomeNavigationService(serviceProvider));

        var navigationService1 = new CompositeNavigationService(
            serviceProvider.GetRequiredService<CloseModalNavigationService>(),
            CreateHomeNavigationService(serviceProvider));

        return new CreateGroupViewModel(
            serviceProvider.GetRequiredService<HttpClient>(),
            navigationService,
            navigationService1);
    }
}