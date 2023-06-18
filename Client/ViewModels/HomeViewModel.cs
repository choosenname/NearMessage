using System;
using Client.Models;
using Client.Stores;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Interfaces;
using System.Windows;
using Client.Commands.Messages;
using Client.Commands.Navigation;
using Client.Commands.Users;
using Client.Services;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    private UserStore _userStore;
    private ChatViewModel? _chatViewModel;
    private bool _isSearching;
    private bool _isLoading;

    private ObservableCollection<ContactModel> _contacts = new();
    private string? _searchText;
    private ContactModel? _selectedContact;

    public HomeViewModel(UserStore userStore, HttpClient httpClient,
        INavigationService settingsNavigationService, INavigationService createGroupNavigationService,
        INavigationService authenticationNavigationService)
    {
        _userStore = userStore;
        _httpClient = httpClient;

        GetLastMessagesCommand = new GetLastMessagesCommand(httpClient, userStore);
        GetAllUsersCommand = new GetUsersCommand(this, httpClient, userStore);
        SearchUserCommand = new SearchUserCommand(this, httpClient);
        SettingsNavigateCommand = new NavigateCommand(settingsNavigationService);
        CreateGroupCommand = new NavigateCommand(createGroupNavigationService);
        CloseSearchCommand = new CloseSearchCommand(this, _httpClient, _userStore);
        LogOutCommand = new LogOutCommand(userStore, authenticationNavigationService);

        IsLoading = true;
        GetAllUsersCommand.Execute(null);

        Task.Run(GetLastMessages);
        Task.Run(UpdateUsers);
    }

    private async Task UpdateUsers()
    {
        while (true)
        {
            if (!IsSearching)
            {
                await Task.Delay(5000);
                GetAllUsersCommand.Execute(null);
            }
        }
    }


    private async Task GetLastMessages()
    {
        while (true)
        {
            GetLastMessagesCommand.Execute(null);
            await Task.Delay(1000);
        }
    }

    public ObservableCollection<ContactModel> Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            OnPropertyChanged(nameof(Contacts));
        }
    }

    public ChatViewModel? ChatViewModel
    {
        get => _chatViewModel;
        set
        {
            _chatViewModel = value;
            OnPropertyChanged(nameof(ChatViewModel));
        }
    }

    public ContactModel? SelectedContact
    {
        get => _selectedContact;
        set
        {
            if (value == _selectedContact || value == null) return;

                _selectedContact = value;
            OnPropertyChanged(nameof(SelectedContact));

            if(_selectedContact != null)
                ChatViewModel = new ChatViewModel(this, _userStore, _httpClient);
        }
    }

    public string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
        }
    }

    public bool IsSearching
    {
        get => _isSearching;
        set
        {
            _isSearching = value;
            OnPropertyChanged(nameof(IsSearching));
        }
    }

    public UserStore UserStore
    {
        get => _userStore;
        set
        {
            _userStore = value;
            OnPropertyChanged(nameof(UserStore));
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }

    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public ICommand GetAllUsersCommand { get; }
    public ICommand SearchUserCommand { get; }
    public ICommand GetLastMessagesCommand { get; }
    public ICommand SettingsNavigateCommand { get; }
    public ICommand CreateGroupCommand { get; }
    public ICommand CloseSearchCommand { get; }
    public ICommand LogOutCommand { get; }

    
}