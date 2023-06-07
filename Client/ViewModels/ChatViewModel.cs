using Client.Commands;
using Client.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private ContactModel _currentContact;

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

    private ObservableCollection<MessageModel> _messages;
    public ObservableCollection<MessageModel> Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    public ICommand SendMessageCommand { get; set; }

    public ChatViewModel(ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;
        SendMessageCommand = new SendMessageCommand(currentContact, MessageText, httpClient);
    }
}
