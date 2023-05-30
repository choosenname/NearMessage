using Client.Commands;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            _serviceProvider = Client.Startup.Configure();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var registrationViewModel = _serviceProvider.GetRequiredService<RegistrationViewModel>();

            var navigationStore = _serviceProvider.GetRequiredService<NavigationStore>();
            navigationStore.CurrentViewModel = registrationViewModel;

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
