using Client.Properties;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using Client.Interfaces;

namespace Client;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = Client.Startup.Configure();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        ViewModelBase viewModel;

        if (string.IsNullOrEmpty(Settings.Default.Token))
        {
            viewModel = _serviceProvider.GetRequiredService<RegistrationViewModel>();
        }
        else
        {
            var httpClient = _serviceProvider.GetRequiredService<HttpClient>();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", 
                    _serviceProvider.GetRequiredService<UserStore>().Token );

            var response = await httpClient.PostAsync("/authentication/confirm", null);

            if (!response.IsSuccessStatusCode)
                viewModel = _serviceProvider.GetRequiredService<AuthenticationViewModel>();
            else
                viewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
        }

        var navigationStore = _serviceProvider.GetRequiredService<INavigationService>();
        navigationStore.Navigate();

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}