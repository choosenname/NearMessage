using System;
using Client.Models;
using Client.Queries;
using Client.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    private readonly UserStore _userStore;
    private ChatViewModel? _chatViewModel;

    private ObservableCollection<ContactModel> _contacts = new();
    private string? _searchText;
    private ContactModel _selectedContact;

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        GetLastMessagesQuery = new GetLastMessagesQuery(httpClient, userStore);
        _selectedContact = new ContactModel(Guid.Empty, String.Empty, null);

        GetAllUsersQuery = new GetUsersQuery(this, httpClient, userStore);
        SearchUserQuery = new SearchUserQuery(this, httpClient);

        GetAllUsersQuery.Execute(null);

        Task.Run(GetLastMessages);
    }

    private async Task GetLastMessages()
    {
        while (true)
        {
            GetLastMessagesQuery.Execute(null);
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

    public ICommand GetAllUsersQuery { get; }
    public ICommand SearchUserQuery { get; }
    public ICommand GetLastMessagesQuery { get; }
}