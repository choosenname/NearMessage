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
    private HttpClient _httpClient;

    private ObservableCollection<UserModel> _users = new();

    public ObservableCollection<UserModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
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

    private UserModel? _selectedUser;
    public UserModel? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));

            ChatViewModel = new ChatViewModel(value, _httpClient);
        }
    }

    public ICommand GetAllUsersQuery { get; }

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        _httpClient = httpClient;
        GetAllUsersQuery = new GetAllUsersQuery(this, httpClient, userStore);
    }

}
