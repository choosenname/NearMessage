using Client.Models;
using Client.Properties;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading;

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
        services.AddSingleton(new UserStore
        {
            User = new UserModel(
                Guid.Empty,
                Settings.Default.Username,
                Settings.Default.Password),
            Token = Settings.Default.Token,
            LastResponseTime = DateTime.Now
        });

        return services.BuildServiceProvider();
    }
}