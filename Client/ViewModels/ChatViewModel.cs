using Client.Models;
using Client.Stores;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Client.Services;
using System;
using Client.Commands.Messages;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private ObservableCollection<MediaModel>? _messages;
    private string _messageText = string.Empty;
    private readonly HomeViewModel _homeViewModel;
    private MediaModel? _selectedMessage;

    public ChatViewModel(HomeViewModel homeViewModel, UserStore userStore, HttpClient httpClient)
    {
        UserStore = userStore;
        _homeViewModel = homeViewModel;

        GetMessagesCommand = new LoadMessagesCommand(this, httpClient);
        SendMessageCommand = new SendMessageCommand(this, _homeViewModel.SelectedContact, httpClient);
        SendMediaCommand = new SendMediaCommand(httpClient, _homeViewModel.SelectedContact, this);
        SaveMediaCommand = new SaveMediaCommand(this);


        GetMessagesCommand.Execute(null);

        SaveEntityModelService.MessagesSaved += OnMessagesSaved;
    }

    private void OnMessagesSaved(object? sender, EventArgs e)
    {
        GetMessagesCommand.Execute(null);
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

    public ObservableCollection<MediaModel>? Messages
    {
        get => _messages;
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }

    public override void Dispose()
    {
        SaveEntityModelService.MessagesSaved -= OnMessagesSaved;

        base.Dispose();
    }

    public MediaModel? SelectedMessage
    {
        get => _selectedMessage;
        set
        {
            _selectedMessage = value;
    OnPropertyChanged(nameof(SelectedMessage));
    SaveMediaCommand.Execute(null);
        }
    }


    public ICommand SendMessageCommand { get; }
    public ICommand GetMessagesCommand { get; }
    public ICommand SendMediaCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand SaveMediaCommand { get; }

}