using Client.Commands;
using Client.Models;
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

    public ChatViewModel(ContactModel currentContact, HttpClient httpClient)
    {
        _currentContact = currentContact;
        SendMessageCommand = new SendMessageCommand(currentContact, Message, httpClient);
    }
}
