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
    private ObservableCollection<MessageModel>? _messages;
    private string _messageText = string.Empty;
    private readonly HomeViewModel _homeViewModel;

    public ChatViewModel(HomeViewModel homeViewModel, UserStore userStore, HttpClient httpClient,
        ref ContactModel contactModel)
    {
        UserStore = userStore;
        _homeViewModel = homeViewModel;

        GetMessagesQuery = new GetMessagesQuery(this, httpClient);
        SendMessageCommand = new SendMessageCommand(this, _homeViewModel.SelectedContact, httpClient);
        SendMediaCommand = new SendMediaCommand(httpClient, _homeViewModel.SelectedContact, this);

        GetMessagesQuery.Execute(null);
    }

    public UserStore UserStore { get; }

    public ContactModel CurrentContact
    {
        get => _homeViewModel.SelectedContact;
        set
        {
            _homeViewModel.SelectedContact = value;
            OnPropertyChanged(nameof(CurrentContact));
        }
    }

    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }

    public ObservableCollection<MessageModel>? Messages
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
    public ICommand RefreshCommand { get; }
}