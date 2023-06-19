using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Client.Commands.Navigation;
using Client.Commands.Users;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.Stores;

namespace Client.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private UserStore _userStore;
    private readonly UserInformationModel _informationModel;
    private ObservableCollection<string> availableThemes;

    public SettingsViewModel(UserStore userStore, INavigationService homeNavigationService)
    {
        _userStore = userStore;
        _informationModel = LoadEntityModelService.LoadEntity(_userStore.User.Id);

        availableThemes = new ObservableCollection<string>()
        {
            "Light",
            "Dark"
        };

        ExitCommand = new ExitCommand(this, homeNavigationService);
    }

    public ICommand ExitCommand { get; }

    public UserStore UserStore
    {
        get => _userStore;
        set
        {
            _userStore = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    public string? About
    {
        get => _informationModel.About;
        set
        {
            _informationModel.About = value;
            OnPropertyChanged(nameof(About));
        }
    }

    public ObservableCollection<string> AvailableThemes
    {
        get => availableThemes;
        set
        {
            availableThemes = value;
            OnPropertyChanged(nameof(AvailableThemes));
        }
    }

    public string SelectedTheme
    {
        get => _userStore.Theme;
        set
        {
            if (_userStore.Theme == value) return;

            _userStore.Theme = value;
            OnPropertyChanged(nameof(SelectedTheme));
        }
    }
}