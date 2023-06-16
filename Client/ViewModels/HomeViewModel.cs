using System;
using Client.Models;
using Client.Stores;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Interfaces;
using System.Windows;
using Client.Commands.Croup;
using Client.Commands.Messages;
using Client.Commands.Navigation;
using Client.Commands.Users;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;
    private ChatViewModel? _chatViewModel;

    private ObservableCollection<ContactModel> _contacts = new();
    private string? _searchText;
    private ContactModel _selectedContact;

    public HomeViewModel(UserStore userStore, HttpClient httpClient, 
        INavigationService settingsNavigationService, INavigationService createGroupNavigationService)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _selectedContact = new ContactModel(Guid.Empty, string.Empty, null);

        GetLastMessagesCommand = new GetLastMessagesCommand(httpClient, userStore);
        GetAllUsersCommand = new GetUsersCommand(this, httpClient, userStore);
        SearchUserCommand = new SearchUserCommand(this, httpClient);
        SettingsNavigateCommand = new NavigateCommand(settingsNavigationService);
        CreateGroupCommand = new NavigateCommand(createGroupNavigationService);

        GetAllUsersCommand.Execute(null);

        Task.Run(GetLastMessages);
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

    public ContactModel SelectedContact
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

    public override void Dispose()
    {
        base.Dispose();
    }

    public ICommand GetAllUsersCommand { get; }
    public ICommand SearchUserCommand { get; }
    public ICommand GetLastMessagesCommand { get; }
    public ICommand SettingsNavigateCommand { get; }
    public ICommand CreateGroupCommand { get; }
}