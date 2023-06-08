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

    public ObservableCollection<ContactModel> Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            OnPropertyChanged(nameof(Contacts));
        }
    }

    private ChatViewModel? _chatViewModel;
    public ChatViewModel? ChatViewModel
    {
        get => _chatViewModel;
        set
        {
            _chatViewModel = value;
            OnPropertyChanged(nameof(ChatViewModel));
        }
    }

    private ContactModel? _selectedContact;
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

    public ICommand GetAllUsersQuery { get; }

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        GetAllUsersQuery = new GetAllUsersQuery(this, httpClient, userStore);
        GetAllUsersQuery.Execute(null);
    }

}
