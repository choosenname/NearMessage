using System;
using Client.Models;
using Client.Stores;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Interfaces;
using System.Windows;
using Client.Commands.Messages;
using Client.Commands.Navigation;
using Client.Commands.Users;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;
    private ChatViewModel? _chatViewModel;
    private bool _isSearching;

    private ObservableCollection<ContactModel> _contacts = new();
    private string? _searchText;
    private ContactModel? _selectedContact;

    public HomeViewModel(UserStore userStore, HttpClient httpClient,
        INavigationService settingsNavigationService, INavigationService createGroupNavigationService)
    {
        _userStore = userStore;
        _httpClient = httpClient;

        GetLastMessagesCommand = new GetLastMessagesCommand(httpClient, userStore);
        GetAllUsersCommand = new GetUsersCommand(this, httpClient, userStore);
        SearchUserCommand = new SearchUserCommand(this, httpClient);
        SettingsNavigateCommand = new NavigateCommand(settingsNavigationService);
        CreateGroupCommand = new NavigateCommand(createGroupNavigationService);
        CloseSearchCommand = new CloseSearchCommand(this, _httpClient, _userStore);


        Task.Run(GetLastMessages);
        Task.Run(UpdateUsers);
    }

    private async Task UpdateUsers()
    {
        while (true)
        {
            if (!IsSearching)
            {
                GetAllUsersCommand.Execute(null);
                await Task.Delay(5000);
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
            _selectedContact = value;
            OnPropertyChanged(nameof(SelectedContact));

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
}