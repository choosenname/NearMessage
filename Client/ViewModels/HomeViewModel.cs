using Client.Commands;
using Client.Models;
using Client.Queries;
using Client.Stores;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    private readonly HttpClient _httpClient;

    private ObservableCollection<ContactModel> _contacts = new();
    private ChatViewModel? _chatViewModel;
    private ContactModel? _selectedContact;
    private string? _searchText;

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

            ChatViewModel = new ChatViewModel(value, _userStore, _httpClient);
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

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        SearchUserQuery = new SearchUserQuery(this, httpClient, userStore);
        GetAllUsersQuery = new GetAllUsersQuery(this, httpClient, userStore);
        GetAllUsersQuery.Execute(null);
    }

}
