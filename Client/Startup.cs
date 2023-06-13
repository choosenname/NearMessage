using Client.Models;
using Client.Properties;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading;
using Client.Services;

namespace Client;

public class Startup
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<RegistrationViewModel>();
        services.AddSingleton<AuthenticationViewModel>();
        services.AddSingleton<HomeViewModel>();

        services.AddSingleton(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton(new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite),
            BaseAddress = new Uri(Settings.Default.HttpUriString)
        });

        services.AddSingleton<NavigationStore>();
        services.AddSingleton<UserStore>();
        //services.AddSingleton(UserStoreSettingsService.GetUserStore());

        return services.BuildServiceProvider();
    }
}