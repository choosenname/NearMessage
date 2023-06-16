using Client.Interfaces;
using Client.Properties;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using Client.Services;

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
        var navigation = _serviceProvider.GetRequiredService<INavigationService>();
        navigation.Navigate();

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}