using Client.Stores;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static HttpClient httpClient;

        private readonly NavigationStore _navigationStore;

        public static HttpClient HttpClient => httpClient;

        public App()
        {
            _navigationStore = new NavigationStore();

            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMilliseconds(System.Threading.Timeout.Infinite),
                BaseAddress = new Uri("https://localhost:7196")
            };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new RegistrationViewModel(_navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
