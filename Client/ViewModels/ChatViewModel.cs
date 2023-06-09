using Client.Commands;
using Client.Models;
using Client.Queries;
using Client.Stores;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private ContactModel _currentContact;

    public UserStore UserStore { get; }

    public ContactModel CurrentContact
    {
        get => _currentContact;
        set
        {
            _currentContact = value;
            OnPropertyChanged(nameof(CurrentContact));
        }
    }

    private string _messageText = string.Empty;

    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }

    private ObservableCollection<MessageModel> _messages = new();
    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    public ICommand SendMessageCommand { get; }
    public ICommand GetMessagesQuery { get; }
    public ICommand SendMediaCommand { get; }

    public ChatViewModel(ContactModel currentContact, UserStore userStore, HttpClient httpClient)
    {
        _currentContact = currentContact;
        UserStore = userStore;
        SendMessageCommand = new SendMessageCommand(this, currentContact, httpClient);
        GetMessagesQuery = new GetMessagesQuery(this, httpClient);
        SendMediaCommand = new SendMediaCommand(httpClient, currentContact, this);
        GetMessagesQuery.Execute(null);
    }
}