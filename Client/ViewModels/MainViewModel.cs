using System;
using System.Windows;
using System.Windows.Input;
using Client.Commands;
using Client.Commands.Users;
using Client.Services;
using Client.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly UserStore _userStore;

    public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,
    UserStore userStore)
    {
        ReloadResources(userStore);

        _navigationStore = navigationStore;
        _modalNavigationStore = modalNavigationStore;
        _userStore = userStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
    }

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;

    public static void ReloadResources(UserStore userStore)
    {
        Application.Current.Resources.MergedDictionaries.Clear();

        string[] resourcePaths = new string[]
        {
            $"Styles/Theme/{userStore.Theme}Theme.xaml",
            "Styles/Colors.xaml",
            "Styles/Theme/TextBoxColors.xaml",
            "Styles/TextBlockStyle.xaml",
            "Styles/TextBoxStyle.xaml",
            "Styles/ButtonStyle.xaml",
            "Styles/BorderStyle.xaml",
            "Styles/LoadingSpinnerStyle.xaml",
            "Styles/Icons.xaml",
            "Styles/PathStyle.xaml"
        };

        foreach (string resourcePath in resourcePaths)
        {
            var resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri(resourcePath, UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }


    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsOpen));
    }

    public void OnWindowClosing()
    {
        UserStoreSettingsService.SaveUserStore(_userStore);
    }
}