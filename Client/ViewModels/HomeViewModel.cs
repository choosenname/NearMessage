using Client.Commands;
using Client.Models;
using Client.Stores;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Documents;
using System.Windows.Input;

namespace Client.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private List<UserModel> _users = new();

    public List<UserModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    public ICommand GetAllUsersCommand { get; }

    public HomeViewModel(UserStore userStore, HttpClient httpClient)
    {
        GetAllUsersCommand = new GetAllUsersCommand(this, httpClient, userStore);
    }
}
