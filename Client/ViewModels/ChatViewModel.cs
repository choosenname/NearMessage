using Client.Commands;
using Client.Models;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private UserModel _currentUser;

    public UserModel CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    private string _message = string.Empty;

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public ICommand SendMessageCommand { get; set; }

    public ChatViewModel(UserModel currentUser, HttpClient httpClient)
    {
        _currentUser = currentUser;
        SendMessageCommand = new SendMessageCommand(currentUser, Message, httpClient);
    }
}
