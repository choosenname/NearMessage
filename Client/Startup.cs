using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Client;

public class Startup
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<RegistrationViewModel>();
        services.AddSingleton<AuthenticationViewModel>();

        services.AddSingleton(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton<HttpClient>(new HttpClient()
        {
            Timeout = TimeSpan.FromMilliseconds(System.Threading.Timeout.Infinite),
            BaseAddress = new Uri("https://localhost:7196")
        });

        services.AddSingleton<NavigationStore>();
        services.AddSingleton<UserStore>(provider => new UserStore()
        {
            User = new Models.UserModel("", "")
        });

        return services.BuildServiceProvider();
    }
}
